using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Arrowgene.MonsterHunterOnline.ClientTools.Level;

namespace Arrowgene.MonsterHunterOnline.UI.Components;

/// <summary>
/// Custom control that renders a 2D top-down view of a level map with spawn points.
/// </summary>
public sealed class MapCanvas : Control
{
    public static readonly StyledProperty<LevelData?> LevelProperty =
        AvaloniaProperty.Register<MapCanvas, LevelData?>(nameof(Level));

    public static readonly StyledProperty<bool> ShowMonstersProperty =
        AvaloniaProperty.Register<MapCanvas, bool>(nameof(ShowMonsters), true);

    public static readonly StyledProperty<bool> ShowResourcesProperty =
        AvaloniaProperty.Register<MapCanvas, bool>(nameof(ShowResources), true);

    public static readonly StyledProperty<bool> ShowPlayerSpawnsProperty =
        AvaloniaProperty.Register<MapCanvas, bool>(nameof(ShowPlayerSpawns), true);

    public static readonly StyledProperty<bool> ShowRegionsProperty =
        AvaloniaProperty.Register<MapCanvas, bool>(nameof(ShowRegions), true);

    public static readonly StyledProperty<bool> UseMinimapProperty =
        AvaloniaProperty.Register<MapCanvas, bool>(nameof(UseMinimap), true);

    private static readonly IBrush BgBrush = new SolidColorBrush(Color.FromRgb(30, 32, 38));
    private static readonly IBrush TerrainBrush = new SolidColorBrush(Color.FromRgb(42, 46, 55));
    private static readonly IPen TerrainPen = new Pen(new SolidColorBrush(Color.FromRgb(60, 65, 78)), 1);
    private static readonly IPen GridPen = new Pen(new SolidColorBrush(Color.FromArgb(30, 255, 255, 255)), 0.5);

    // Entity colors
    private static readonly IBrush MonsterBrush = new SolidColorBrush(Color.FromRgb(239, 68, 68));
    private static readonly IBrush GenericSpawnBrush = new SolidColorBrush(Color.FromRgb(251, 146, 60));
    private static readonly IBrush NpcBrush = new SolidColorBrush(Color.FromRgb(96, 165, 250));
    private static readonly IBrush ResourceBrush = new SolidColorBrush(Color.FromRgb(74, 222, 128));
    private static readonly IBrush PlayerSpawnBrush = new SolidColorBrush(Color.FromRgb(250, 204, 21));
    private static readonly IBrush BossMarkerBrush = new SolidColorBrush(Color.FromRgb(168, 85, 247));

    private static readonly IPen RegionPen = new Pen(new SolidColorBrush(Color.FromArgb(80, 96, 165, 250)), 1.5);
    private static readonly IBrush RegionFill = new SolidColorBrush(Color.FromArgb(15, 96, 165, 250));
    private static readonly IBrush ClientMinimapRegionFill = new SolidColorBrush(Color.FromArgb(22, 250, 204, 21));
    private static readonly IPen ClientMinimapRegionPen = new Pen(new SolidColorBrush(Color.FromArgb(110, 250, 204, 21)), 1);

    private static readonly IBrush MarkerBrush = new SolidColorBrush(Color.FromRgb(56, 189, 248));
    private static readonly IPen MarkerPen = new Pen(new SolidColorBrush(Color.FromArgb(200, 255, 255, 255)), 1);

    private static readonly Typeface LabelTypeface = new("Inter, Segoe UI, sans-serif");

    // Transform state
    private float _offsetX;
    private float _offsetY;
    private float _zoom = 1f;
    private Point _lastPan;
    private bool _isPanning;

    // Heightmap bitmap
    private WriteableBitmap? _terrainBitmap;
    private List<(LevelClientMiniMapBitmapPlacement placement, Bitmap bitmap)>? _minimapBitmaps;

    // Tooltip
    private LevelEntity? _hoveredEntity;

    public LevelData? Level
    {
        get => GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public bool ShowMonsters
    {
        get => GetValue(ShowMonstersProperty);
        set => SetValue(ShowMonstersProperty, value);
    }

    public bool ShowResources
    {
        get => GetValue(ShowResourcesProperty);
        set => SetValue(ShowResourcesProperty, value);
    }

    public bool ShowPlayerSpawns
    {
        get => GetValue(ShowPlayerSpawnsProperty);
        set => SetValue(ShowPlayerSpawnsProperty, value);
    }

    public bool ShowRegions
    {
        get => GetValue(ShowRegionsProperty);
        set => SetValue(ShowRegionsProperty, value);
    }

    public bool UseMinimap
    {
        get => GetValue(UseMinimapProperty);
        set => SetValue(UseMinimapProperty, value);
    }

    static MapCanvas()
    {
        AffectsRender<MapCanvas>(LevelProperty, ShowMonstersProperty, ShowResourcesProperty,
            ShowPlayerSpawnsProperty, ShowRegionsProperty, UseMinimapProperty);
    }

    public MapCanvas()
    {
        ClipToBounds = true;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == LevelProperty)
        {
            RebuildTerrainBitmap();
            RebuildMinimapBitmaps();
            ResetView();
        }
        else if (change.Property == UseMinimapProperty)
        {
            ResetView();
        }
    }

    private void RebuildTerrainBitmap()
    {
        _terrainBitmap?.Dispose();
        _terrainBitmap = null;

        LevelData? level = Level;
        if (level?.HeightmapPixels == null || level.HeightmapWidth <= 0) return;

        try
        {
            var bmp = new WriteableBitmap(
                new PixelSize(level.HeightmapWidth, level.HeightmapHeight),
                new Vector(96, 96),
                Avalonia.Platform.PixelFormat.Bgra8888,
                AlphaFormat.Premul);

            using (var fb = bmp.Lock())
            {
                System.Runtime.InteropServices.Marshal.Copy(
                    level.HeightmapPixels, 0, fb.Address, level.HeightmapPixels.Length);
            }

            _terrainBitmap = bmp;
        }
        catch
        {
            // Ignore bitmap creation failures
        }
    }

    private void RebuildMinimapBitmaps()
    {
        if (_minimapBitmaps != null)
        {
            foreach (var (_, bitmap) in _minimapBitmaps)
            {
                bitmap.Dispose();
            }

            _minimapBitmaps = null;
        }

        List<LevelClientMiniMapBitmapPlacement>? placements = Level?.ClientMiniMap?.BitmapPlacements;
        if (placements == null || placements.Count == 0)
        {
            return;
        }

        _minimapBitmaps = [];
        foreach (LevelClientMiniMapBitmapPlacement placement in placements)
        {
            if (placement.ImageData.Length == 0)
            {
                continue;
            }

            try
            {
                using MemoryStream stream = new(placement.ImageData, writable: false);
                Bitmap bitmap = new(stream);
                _minimapBitmaps.Add((placement, bitmap));
            }
            catch
            {
                // Skip unsupported image data.
            }
        }
    }

    public void ResetView()
    {
        _zoom = 1f;
        _offsetX = 0;
        _offsetY = 0;
        FitToView();
        InvalidateVisual();
    }

    private void FitToView()
    {
        LevelData? level = Level;
        if (level == null) return;

        float margin = 40;
        float availW = (float)Bounds.Width - margin * 2;
        float availH = (float)Bounds.Height - margin * 2;
        if (availW <= 0 || availH <= 0) return;

        Size contentSize = GetContentSize(level);
        _zoom = Math.Min(availW / (float)contentSize.Width, availH / (float)contentSize.Height);
        _offsetX = (float)Bounds.Width / 2f - (float)contentSize.Width / 2f * _zoom;
        _offsetY = (float)Bounds.Height / 2f - (float)contentSize.Height / 2f * _zoom;
    }

    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        if (Level != null && _zoom <= 1f)
        {
            FitToView();
        }

        InvalidateVisual();
    }

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);
        Point pos = e.GetPosition(this);
        float oldZoom = _zoom;
        float factor = e.Delta.Y > 0 ? 1.15f : 1f / 1.15f;
        _zoom = Math.Clamp(_zoom * factor, 0.01f, 100f);

        // Zoom towards cursor
        _offsetX = (float)pos.X - ((float)pos.X - _offsetX) * (_zoom / oldZoom);
        _offsetY = (float)pos.Y - ((float)pos.Y - _offsetY) * (_zoom / oldZoom);

        InvalidateVisual();
        e.Handled = true;
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (e.GetCurrentPoint(this).Properties.IsMiddleButtonPressed ||
            e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            _isPanning = true;
            _lastPan = e.GetPosition(this);
            e.Handled = true;
        }
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        _isPanning = false;
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        if (_isPanning)
        {
            Point pos = e.GetPosition(this);
            _offsetX += (float)(pos.X - _lastPan.X);
            _offsetY += (float)(pos.Y - _lastPan.Y);
            _lastPan = pos;
            InvalidateVisual();
            e.Handled = true;
            return;
        }

        // Hit test for tooltip
        LevelData? level = Level;
        if (level == null) return;

        Point mousePos = e.GetPosition(this);
        LevelEntity? closest = null;
        double closestDist = 8; // pixel threshold

        foreach (LevelEntity ent in level.Entities)
        {
            if (!ShouldRender(ent)) continue;
            if (!TryProjectToCanvas(level, ent.Pos, out Point projected))
            {
                continue;
            }

            float sx = (float)projected.X;
            float sy = (float)projected.Y;
            double dist = Math.Sqrt((mousePos.X - sx) * (mousePos.X - sx) + (mousePos.Y - sy) * (mousePos.Y - sy));
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = ent;
            }
        }

        if (closest != _hoveredEntity)
        {
            _hoveredEntity = closest;
            if (closest != null)
            {
                string tip = $"{closest.Name}\nClass: {closest.EntityClass}";
                if (closest.FixedMonsterID > 0)
                    tip += $"\nMonsterID: {closest.FixedMonsterID}";
                if (closest.RegionId >= 0)
                    tip += $"\nRegion: {closest.RegionId}";
                tip += $"\nPos: {closest.Pos}";
                if (level.ClientMiniMap?.TryProjectWorldPosition(closest.Pos, out Vec2 mapPos) == true)
                    tip += $"\nMiniMap: {mapPos}";
                ToolTip.SetTip(this, tip);
                ToolTip.SetIsOpen(this, true);
            }
            else
            {
                ToolTip.SetIsOpen(this, false);
                ToolTip.SetTip(this, null);
            }
        }
    }

    public override void Render(DrawingContext ctx)
    {
        base.Render(ctx);
        ctx.FillRectangle(BgBrush, new Rect(Bounds.Size));

        LevelData? level = Level;
        if (level == null)
        {
            var ft = new FormattedText("Select a level", CultureInfo, FlowDirection.LeftToRight,
                LabelTypeface, 16, Brushes.Gray);
            ctx.DrawText(ft, new Point(Bounds.Width / 2 - ft.Width / 2, Bounds.Height / 2 - ft.Height / 2));
            return;
        }

        ClientMinimapProjection projection = default;
        bool useClientMinimap = UseMinimap && TryGetClientMinimapProjection(level, out projection);
        Size contentSize = GetContentSize(level);
        Rect mapRect = new Rect(_offsetX, _offsetY, contentSize.Width * _zoom, contentSize.Height * _zoom);

        DrawBackground(ctx, level, projection, useClientMinimap, mapRect);

        // Mission region polygons only make sense in continuous world-space rendering.
        // The client minimap uses authored 2D regions/links/points instead of projecting these polygons.
        if (ShowRegions && !useClientMinimap)
        {
            foreach (LevelRegion region in level.Regions)
            {
                if (region.Points.Count < 3) continue;
                List<Point> points = [];
                foreach (Vec3 point in region.Points)
                {
                    if (TryProjectToCanvas(level, point, out Point projected))
                    {
                        points.Add(projected);
                    }
                }

                if (points.Count < 3) continue;
                var geo = new StreamGeometry();
                using (var sgCtx = geo.Open())
                {
                    sgCtx.BeginFigure(points[0], true);
                    for (int i = 1; i < points.Count; i++)
                        sgCtx.LineTo(points[i]);
                    sgCtx.EndFigure(true);
                }

                ctx.DrawGeometry(RegionFill, RegionPen, geo);

                // Region label
                var center = new Point(points.Average(p => p.X), points.Average(p => p.Y));
                var label = new FormattedText($"R{region.Id}", CultureInfo, FlowDirection.LeftToRight,
                    LabelTypeface, 10, Brushes.White);
                ctx.DrawText(label, new Point(center.X - label.Width / 2, center.Y - label.Height / 2));
            }
        }

        // Entities
        foreach (LevelEntity ent in level.Entities)
        {
            if (!ShouldRender(ent)) continue;

            if (!TryProjectToCanvas(level, ent.Pos, out Point projected))
            {
                continue;
            }

            float sx = (float)projected.X;
            float sy = (float)projected.Y;
            float radius = GetEntityRadius(ent);
            IBrush brush = GetEntityBrush(ent);

            ctx.DrawEllipse(brush, null, new Point(sx, sy), radius, radius);
        }

        // Legend
        DrawLegend(ctx);
    }

    private bool ShouldRender(LevelEntity ent)
    {
        return ent.EntityClass switch
        {
            "MHMonsterSpawnPoint" => ShowMonsters,
            "MHCollectSpawner" => ShowResources,
            "MHPlayerSpawnPoint" => ShowPlayerSpawns,
            _ => false,
        };
    }

    private static float GetEntityRadius(LevelEntity ent)
    {
        if (ent.EntityClass == "MHPlayerSpawnPoint") return 5f;
        if (ent.OnlyForBoss) return 6f;
        if (ent.FixedMonsterID >= 30000 && ent.FixedMonsterID < 40000) return 4f; // NPC
        return 3.5f;
    }

    private static IBrush GetEntityBrush(LevelEntity ent)
    {
        return ent.EntityClass switch
        {
            "MHPlayerSpawnPoint" => PlayerSpawnBrush,
            "MHCollectSpawner" => ResourceBrush,
            "MHMonsterSpawnPoint" when ent.OnlyForBoss => BossMarkerBrush,
            "MHMonsterSpawnPoint" when ent.FixedMonsterID >= 30000 && ent.FixedMonsterID < 40000 => NpcBrush,
            "MHMonsterSpawnPoint" when ent.FixedMonsterID > 0 => MonsterBrush,
            "MHMonsterSpawnPoint" => GenericSpawnBrush,
            _ => GenericSpawnBrush,
        };
    }

    private void DrawLegend(DrawingContext ctx)
    {
        double lx = 12, ly = Bounds.Height - 120;
        var bgRect = new Rect(lx - 4, ly - 4, 160, 118);
        ctx.FillRectangle(new SolidColorBrush(Color.FromArgb(180, 20, 20, 25)), bgRect);
        ctx.DrawRectangle(new Pen(new SolidColorBrush(Color.FromArgb(60, 255, 255, 255)), 0.5), bgRect);

        DrawLegendItem(ctx, ref ly, lx, MonsterBrush, "Monster Spawn");
        DrawLegendItem(ctx, ref ly, lx, NpcBrush, "NPC");
        DrawLegendItem(ctx, ref ly, lx, GenericSpawnBrush, "Generic Spawn");
        DrawLegendItem(ctx, ref ly, lx, ResourceBrush, "Resource Node");
        DrawLegendItem(ctx, ref ly, lx, PlayerSpawnBrush, "Player Spawn");
        DrawLegendItem(ctx, ref ly, lx, BossMarkerBrush, "Boss Marker");
    }

    private static void DrawLegendItem(DrawingContext ctx, ref double y, double x, IBrush brush, string label)
    {
        ctx.DrawEllipse(brush, null, new Point(x + 6, y + 7), 4, 4);
        var ft = new FormattedText(label, CultureInfo, FlowDirection.LeftToRight,
            LabelTypeface, 11, Brushes.White);
        ctx.DrawText(ft, new Point(x + 16, y));
        y += 17;
    }

    private void DrawBackground(DrawingContext ctx, LevelData level, ClientMinimapProjection projection,
        bool useClientMinimap, Rect mapRect)
    {
        ctx.FillRectangle(TerrainBrush, mapRect);

        if (useClientMinimap)
        {
            DrawMinimapBitmaps(ctx, projection);
            DrawClientMinimapGrid(ctx, projection, mapRect);

            if (ShowRegions)
            {
                foreach (LevelClientMiniMapRegion region in projection.Asset!.Regions)
                {
                    Rect regionRect = GetClientMinimapRegionRect(region, projection);
                    ctx.DrawRectangle(ClientMinimapRegionFill, ClientMinimapRegionPen, regionRect);

                    if (region.IdentifierValue.HasValue)
                    {
                        string labelText = region.IdentifierName.Length > 0
                            ? $"{region.IdentifierName}:{region.IdentifierValue.Value}"
                            : region.IdentifierValue.Value.ToString(CultureInfo);
                        var label = new FormattedText(labelText, CultureInfo, FlowDirection.LeftToRight,
                            LabelTypeface, 10, Brushes.White);
                        ctx.DrawText(label, new Point(regionRect.X + 4, regionRect.Y + 4));
                    }
                }
            }

            DrawMarkers(ctx, projection);

            var title = new FormattedText($"Client minimap: {projection.Asset.AssetName}", CultureInfo,
                FlowDirection.LeftToRight, LabelTypeface, 11, Brushes.White);
            ctx.DrawText(title, new Point(mapRect.X + 8, mapRect.Y + 8));
        }
        else if (_terrainBitmap != null)
        {
            ctx.DrawImage(_terrainBitmap, mapRect);
        }

        ctx.DrawRectangle(TerrainPen, mapRect);
    }

    private void DrawClientMinimapGrid(DrawingContext ctx, ClientMinimapProjection projection, Rect mapRect)
    {
        float gridStep = 32f * _zoom;
        if (gridStep <= 8f)
        {
            return;
        }

        for (float x = 0; x <= projection.Width; x += 32f)
        {
            float sx = _offsetX + x * _zoom;
            ctx.DrawLine(GridPen, new Point(sx, mapRect.Y), new Point(sx, mapRect.Bottom));
        }

        for (float y = 0; y <= projection.Height; y += 32f)
        {
            float sy = _offsetY + y * _zoom;
            ctx.DrawLine(GridPen, new Point(mapRect.X, sy), new Point(mapRect.Right, sy));
        }
    }

    private void DrawMinimapBitmaps(DrawingContext ctx, ClientMinimapProjection projection)
    {
        if (_minimapBitmaps == null)
        {
            return;
        }

        // All bitmaps are positioned in FLA canvas space (Y-down).
        // Compute the screen position of FLA origin (0,0).
        float flaOriginScreenX = _offsetX + (0f - projection.MinX) * _zoom;
        float flaOriginScreenY = _offsetY + (0f - projection.MinY) * _zoom;

        foreach (var (placement, bitmap) in _minimapBitmaps)
        {
            Vec2 topLeft = placement.Transform.TransformPoint(0f, 0f);
            float screenX = flaOriginScreenX + topLeft.X * _zoom;
            float screenY = flaOriginScreenY + topLeft.Y * _zoom;

            // Use transform-derived canvas size (handles scale transforms).
            Vec2 topRight = placement.Transform.TransformPoint(placement.Width, 0f);
            Vec2 bottomLeft = placement.Transform.TransformPoint(0f, placement.Height);
            float canvasW = MathF.Abs(topRight.X - topLeft.X);
            float canvasH = MathF.Abs(bottomLeft.Y - topLeft.Y);
            if (canvasW < 0.5f) canvasW = placement.Width;
            if (canvasH < 0.5f) canvasH = placement.Height;

            Rect destRect = new(screenX, screenY, canvasW * _zoom, canvasH * _zoom);
            ctx.DrawImage(bitmap, destRect);
        }
    }

    private void DrawMarkers(DrawingContext ctx, ClientMinimapProjection projection)
    {
        List<LevelClientMiniMapMarker>? markers = Level?.ClientMiniMap?.Markers;
        if (markers == null || markers.Count == 0)
        {
            return;
        }

        // FLA canvas uses Y-down, same as the minimap 2D coordinate space.
        float flaOriginScreenX = _offsetX + (0f - projection.MinX) * _zoom;
        float flaOriginScreenY = _offsetY + (0f - projection.MinY) * _zoom;

        foreach (LevelClientMiniMapMarker marker in markers)
        {
            float screenX = flaOriginScreenX + marker.Position2D.X * _zoom;
            float screenY = flaOriginScreenY + marker.Position2D.Y * _zoom;
            Point screenPos = new(screenX, screenY);

            ctx.DrawEllipse(MarkerBrush, MarkerPen, screenPos, 5, 5);

            string text = marker.DisplayText;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var ft = new FormattedText(text, CultureInfo, FlowDirection.LeftToRight,
                    LabelTypeface, 9, Brushes.White);
                ctx.DrawText(ft, new Point(screenPos.X + 7, screenPos.Y - ft.Height / 2));
            }
        }
    }

    private Rect GetClientMinimapRegionRect(LevelClientMiniMapRegion region, ClientMinimapProjection projection)
    {
        Point topLeft = ClientMapToScreen(region.TopLeft2D, projection);
        Point bottomRight = ClientMapToScreen(region.BottomRight2D, projection);
        double left = Math.Min(topLeft.X, bottomRight.X);
        double top = Math.Min(topLeft.Y, bottomRight.Y);
        double width = Math.Abs(bottomRight.X - topLeft.X);
        double height = Math.Abs(bottomRight.Y - topLeft.Y);
        return new Rect(left, top, width, height);
    }

    private bool TryProjectToCanvas(LevelData level, Vec3 worldPosition, out Point point)
    {
        if (UseMinimap && TryGetClientMinimapProjection(level, out ClientMinimapProjection projection))
        {
            if (projection.Asset!.TryProjectWorldPosition(worldPosition, out Vec2 mapPosition))
            {
                point = ClientMapToScreen(mapPosition, projection);
                return true;
            }

            // Entity is not in any minimap region — skip it.
            point = default;
            return false;
        }

        // Terrain mode: project using world coordinates.
        float worldSize = level.Terrain.WorldSize;
        if (worldSize <= 0)
        {
            worldSize = 1024;
        }

        point = new Point(
            worldPosition.X * _zoom + _offsetX,
            (worldSize - worldPosition.Y) * _zoom + _offsetY);
        return true;
    }

    private Size GetContentSize(LevelData level)
    {
        if (UseMinimap && TryGetClientMinimapProjection(level, out ClientMinimapProjection projection))
        {
            return new Size(projection.Width, projection.Height);
        }

        float worldSize = level.Terrain.WorldSize;
        if (worldSize <= 0)
        {
            worldSize = 1024;
        }

        return new Size(worldSize, worldSize);
    }

    private Point ClientMapToScreen(Vec2 mapPosition, ClientMinimapProjection projection)
    {
        // The minimap 2D coordinates from the config are in FLA canvas space (Y-down).
        // No Y-flip needed — mapY=0 is the top of the canvas.
        return new Point(
            (mapPosition.X - projection.MinX) * _zoom + _offsetX,
            (mapPosition.Y - projection.MinY) * _zoom + _offsetY);
    }

    private static bool TryGetClientMinimapProjection(LevelData level, out ClientMinimapProjection projection)
    {
        LevelClientMiniMapAsset? asset = level.ClientMiniMap;
        if (asset == null)
        {
            projection = default;
            return false;
        }

        // No regions and no canvas dimensions — nothing to project.
        if (asset.Regions.Count == 0 && asset.CanvasWidth <= 0 && asset.CanvasHeight <= 0)
        {
            projection = default;
            return false;
        }

        float minX = 0f;
        float maxX = Math.Max(1f, asset.CanvasWidth);
        float minY = 0f;
        float maxY = Math.Max(1f, asset.CanvasHeight);

        if (asset.Regions.Count > 0)
        {
            IEnumerable<Vec2> corners = asset.Regions.SelectMany(region => new[] { region.TopLeft2D, region.BottomRight2D });
            float regionMinX = corners.Min(point => point.X);
            float regionMaxX = corners.Max(point => point.X);
            float regionMinY = corners.Min(point => point.Y);
            float regionMaxY = corners.Max(point => point.Y);

            minX = Math.Min(minX, regionMinX);
            maxX = Math.Max(maxX, regionMaxX);
            minY = Math.Min(minY, regionMinY);
            maxY = Math.Max(maxY, regionMaxY);
        }

        float width = Math.Max(1f, maxX - minX);
        float height = Math.Max(1f, maxY - minY);

        projection = new ClientMinimapProjection(asset, minX, minY, maxX, maxY, width, height);
        return true;
    }

    private static readonly System.Globalization.CultureInfo CultureInfo =
        System.Globalization.CultureInfo.InvariantCulture;

    private readonly record struct ClientMinimapProjection(
        LevelClientMiniMapAsset Asset,
        float MinX,
        float MinY,
        float MaxX,
        float MaxY,
        float Width,
        float Height);
}
