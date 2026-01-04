using Editor.Panels;
using ImGuiNET;
using OpenTK.Mathematics;
using Vector2 = System.Numerics.Vector2;
using Vector4 = OpenTK.Mathematics.Vector4;

namespace Dusty_Easel.Panels;

public class PixelCanvasPanel(PixelRenderer _renderer) : IRenderablePanel
{
    private bool _isDrawing;
    private Vector2i _lastPixelPos;

    public static int CanvasWidth = 64;
    public static int CanvasHeight = 64;
    public static int PixelScale = 8;
    public static bool ShowGrid = true;

    private Vector2 _panOffset = Vector2.Zero;
    private bool _isPanning;
    private Vector2 _lastPanMousePos;

  

    public void Render()
    {
        if (_renderer == null)
            return;

        ImGui.Begin("Pixel Canvas");

        var displaySize = new Vector2(
            _renderer.TextureSize.X * PixelScale,
            _renderer.TextureSize.Y * PixelScale
        );

        var canvasPos = ImGui.GetCursorScreenPos();
        var availSize = ImGui.GetContentRegionAvail();

        ClampPanOffset(displaySize, availSize);

        var adjustedCanvasPos = new Vector2(
            canvasPos.X + _panOffset.X,
            canvasPos.Y + _panOffset.Y
        );

        var drawList = ImGui.GetWindowDrawList();

        drawList.AddImage(
            _renderer.TextureId,
            adjustedCanvasPos,
            new Vector2(adjustedCanvasPos.X + displaySize.X, adjustedCanvasPos.Y + displaySize.Y),
            new Vector2(0, 0),
            new Vector2(1, 1)
        );

        var borderColor = ImGui.GetColorU32(new System.Numerics.Vector4(0.7f, 0.7f, 0.7f, 1.0f));
        drawList.AddRect(
            adjustedCanvasPos,
            new Vector2(adjustedCanvasPos.X + displaySize.X, adjustedCanvasPos.Y + displaySize.Y),
            borderColor,
            0, 0, 2.0f
        );

        if (ShowGrid && PixelScale >= 4)
        {
            var gridColor = ImGui.GetColorU32(new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 0.3f));

            for (var x = 0; x <= _renderer.TextureSize.X; x++)
            {
                var xPos = adjustedCanvasPos.X + x * PixelScale;
                drawList.AddLine(
                    new Vector2(xPos, adjustedCanvasPos.Y),
                    new Vector2(xPos, adjustedCanvasPos.Y + displaySize.Y),
                    gridColor
                );
            }

            for (var y = 0; y <= _renderer.TextureSize.Y; y++)
            {
                var yPos = adjustedCanvasPos.Y + y * PixelScale;
                drawList.AddLine(
                    new Vector2(adjustedCanvasPos.X, yPos),
                    new Vector2(adjustedCanvasPos.X + displaySize.X, yPos),
                    gridColor
                );
            }
        }

        ImGui.SetCursorScreenPos(canvasPos);
        availSize = ImGui.GetContentRegionAvail();
        ImGui.InvisibleButton("canvas", availSize);

        if (ImGui.IsItemHovered())
        {
            var mousePos = ImGui.GetMousePos();

            if (ImGui.IsMouseClicked(ImGuiMouseButton.Middle))
            {
                _isPanning = true;
                _lastPanMousePos = mousePos;
            }
        }

        if (_isPanning)
        {
            var mousePos = ImGui.GetMousePos();
            var delta = new Vector2(
                mousePos.X - _lastPanMousePos.X,
                mousePos.Y - _lastPanMousePos.Y
            );

            _panOffset.X += delta.X;
            _panOffset.Y += delta.Y;

            _lastPanMousePos = mousePos;

            if (!ImGui.IsMouseDown(ImGuiMouseButton.Middle)) _isPanning = false;
        }

        if (ImGui.IsItemHovered() && !_isPanning)
        {
            var mousePos = ImGui.GetMousePos();
            var pixelPos = ScreenToPixel(mousePos, adjustedCanvasPos);

            if (pixelPos.X >= 0 && pixelPos.X < _renderer.TextureSize.X &&
                pixelPos.Y >= 0 && pixelPos.Y < _renderer.TextureSize.Y)
            {
                var highlightColor = ImGui.GetColorU32(new System.Numerics.Vector4(1, 1, 1, 0.5f));
                var highlightPos = new Vector2(
                    adjustedCanvasPos.X + pixelPos.X * PixelScale,
                    adjustedCanvasPos.Y + pixelPos.Y * PixelScale
                );
                drawList.AddRect(
                    highlightPos,
                    new Vector2(highlightPos.X + PixelScale, highlightPos.Y + PixelScale),
                    highlightColor,
                    0, 0, 2.0f
                );
            }

            HandleInput(pixelPos);

            ImGui.BeginTooltip();
            ImGui.Text($"Pixel: ({pixelPos.X}, {pixelPos.Y})");
            ImGui.EndTooltip();
        }
        else
        {
            if (ImGui.IsMouseReleased(ImGuiMouseButton.Left)) _isDrawing = false;
        }

        if (ImGui.IsItemHovered())
        {
            var wheel = ImGui.GetIO().MouseWheel;
            if (wheel != 0)
            {
                var oldScale = PixelScale;

                PixelScale = wheel > 0 ? Math.Min(PixelScale + 1, 32) : Math.Max(PixelScale - 1, 1);

                if (oldScale != PixelScale)
                {
                    var mousePos = ImGui.GetMousePos();
                    var relativePos = new Vector2(
                        mousePos.X - adjustedCanvasPos.X,
                        mousePos.Y - adjustedCanvasPos.Y
                    );

                    var scaleRatio = (float)PixelScale / oldScale;
                    _panOffset.X = mousePos.X - canvasPos.X - relativePos.X * scaleRatio;
                    _panOffset.Y = mousePos.Y - canvasPos.Y - relativePos.Y * scaleRatio;
                }
            }
        }

        if (ImGui.IsKeyPressed(ImGuiKey.Home) || ImGui.IsKeyPressed(ImGuiKey.R))
        {
            _panOffset = Vector2.Zero;
            PixelScale = 8;
        }

        ImGui.End();
    }

    private void HandleInput(Vector2i pixelPos)
    {
        if (!ImGui.IsMouseDown(ImGuiMouseButton.Left))
        {
            if (_isDrawing) _isDrawing = false;

            return;
        }

        var color = new Vector4(SettingPanel.BrushColor.X, SettingPanel.BrushColor.Y, SettingPanel.BrushColor.Z,
            SettingPanel.BrushColor.W);

        switch (SettingPanel.CurrentTool)
        {
            case DrawTool.Pencil:
                if (_isDrawing)
                {
                    _renderer?.DrawLine(_lastPixelPos, pixelPos, color);
                }
                else
                {
                    if (SettingPanel.BrushSize == 1)
                        _renderer?.SetPixel(pixelPos.X, pixelPos.Y, color);
                }

                _lastPixelPos = pixelPos;
                _isDrawing = true;
                break;
            
            case DrawTool.Line:
            case DrawTool.Circle:
            case DrawTool.Rectangle:
                if (!_isDrawing) 
                    _isDrawing = true;
                break;
        }
    }

    private static Vector2i ScreenToPixel(Vector2 screenPos, Vector2 canvasPos)
    {
        var x = (int)((screenPos.X - canvasPos.X) / PixelScale);
        var y = (int)((screenPos.Y - canvasPos.Y) / PixelScale);
        return new Vector2i(x, y);
    }

    private void ClampPanOffset(Vector2 canvasSize, Vector2 viewportSize)
    {
        if (canvasSize.X <= viewportSize.X && canvasSize.Y <= viewportSize.Y)
        {
            var margin = Math.Min(canvasSize.X, canvasSize.Y) * 0.5f;

            _panOffset.X = Math.Clamp(_panOffset.X, -canvasSize.X + margin, viewportSize.X - margin);
            _panOffset.Y = Math.Clamp(_panOffset.Y, -canvasSize.Y + margin, viewportSize.Y - margin);
        }
        else
        {
            var minVisible = Math.Max(100f, Math.Min(canvasSize.X, canvasSize.Y) * 0.1f);

            var minPanX = -(canvasSize.X - minVisible);
            var maxPanX = viewportSize.X - minVisible;

            var minPanY = -(canvasSize.Y - minVisible);
            var maxPanY = viewportSize.Y - minVisible;

            _panOffset.X = Math.Clamp(_panOffset.X, minPanX, maxPanX);
            _panOffset.Y = Math.Clamp(_panOffset.Y, minPanY, maxPanY);
        }
    }
}