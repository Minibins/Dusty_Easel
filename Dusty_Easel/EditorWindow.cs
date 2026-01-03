using Editor.ImGuI.Impl;
using ImGuiNET;
using OpenTK.Graphics.OpenGL.Compatibility;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Vector2 = System.Numerics.Vector2;

namespace Dusty_Easel;

public class EditorWindow : GameWindow
{
    private PixelRenderer? _pixelRenderer;

    public EditorWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
        Title = nativeWindowSettings.Title;
        CursorState = CursorState.Normal;
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.Viewport(0, 0, FramebufferSize.X, FramebufferSize.Y);

        EditorImGuiHelper.ImGuiInit(this);

        _pixelRenderer = new PixelRenderer();
        EditorImGuiHelper.SetPixelRenderer(_pixelRenderer);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        GL.Viewport(0, 0, FramebufferSize.X, FramebufferSize.Y);
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        EditorImGuiHelper.ImGuiRender(this);

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {
        _pixelRenderer?.Dispose();

        ImguiImplOpenGL3.Shutdown();
        ImguiImplOpenTK4.Shutdown();
        ImGui.DestroyContext();

        base.OnUnload();
    }
}

public static class EditorImGuiHelper
{
    private static RendererUI? _renderUI;

    public static void ImGuiInit(GameWindow window)
    {
        ImGui.CreateContext();
        ImguiImplOpenTK4.Init(window);
        ImguiImplOpenGL3.Init();

        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
    }

    public static void SetPixelRenderer(PixelRenderer renderer) => _renderUI = new RendererUI(renderer);

    public static void ImGuiRender(GameWindow window)
    {
        ImguiImplOpenGL3.NewFrame();
        ImguiImplOpenTK4.NewFrame();
        ImGui.NewFrame();

        var viewport = ImGui.GetMainViewport();

        ImGui.SetNextWindowPos(viewport.Pos);
        ImGui.SetNextWindowSize(viewport.Size);
        ImGui.SetNextWindowViewport(viewport.ID);

        const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.MenuBar |
                                             ImGuiWindowFlags.NoDocking |
                                             ImGuiWindowFlags.NoTitleBar |
                                             ImGuiWindowFlags.NoCollapse |
                                             ImGuiWindowFlags.NoResize |
                                             ImGuiWindowFlags.NoMove |
                                             ImGuiWindowFlags.NoBringToFrontOnFocus |
                                             ImGuiWindowFlags.NoNavFocus;

        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));

        ImGui.Begin("DockSpace", windowFlags);
        ImGui.PopStyleVar(3);

        var dockspaceId = ImGui.GetID("MainDockSpace");
        ImGui.DockSpace(dockspaceId, new Vector2(0, 0), ImGuiDockNodeFlags.None);

        _renderUI?.Render(window);

        ImGui.End();

        ImGui.Render();
        ImguiImplOpenGL3.RenderDrawData(ImGui.GetDrawData());
    }
}

internal static class Program
{
    private static void Main()
    {
        var window = new EditorWindow(
            GameWindowSettings.Default,
            new NativeWindowSettings
            {
                ClientSize = new Vector2i(1024, 1080),
                Title = "Dusty Easel",
            });

        window.Run();
    }
}