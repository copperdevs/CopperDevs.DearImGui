using System.Runtime.InteropServices;
using CopperDevs.DearImGui.Rendering;
using CopperDevs.DearImGui.Resources;
using CopperDevs.DearImGui.Utility;
using ImGuiNET;

namespace CopperDevs.DearImGui;

public static partial class CopperImGui
{
    /// <summary>
    ///     Load the default font, font awesome icons, as well as invoke the <see cref="ImGuiRenderer.LoadUserFonts" />
    ///     callback
    /// </summary>
    public static void LoadFonts()
    {
        ImGuiRenderer.LoadUserFonts?.Invoke();
        ImGui.GetIO().Fonts.AddFontDefault();
        LoadFontAwesomeIcons();
    }

    /// <summary>
    ///     Load a font from the disc
    /// </summary>
    /// <param name="path">The path of the TTF font on disc</param>
    /// <param name="pixelSize">pixelSize</param>
    /// <remarks>Only TTF fonts are supported</remarks>
    public static void LoadFont(string path, float pixelSize)
    {
        try
        {
            ImGui.GetIO().Fonts.AddFontFromFileTTF(path, pixelSize);
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    /// <summary>
    ///     Load a font from memory
    /// </summary>
    /// <param name="fontData">The data of the TTF font</param>
    /// <param name="pixelSize">pixelSize</param>
    /// <param name="dataSize">dataSize</param>
    /// <remarks>Only TTF fonts are supported</remarks>
    public static void LoadFontFromMemory(byte[] fontData, int pixelSize, int dataSize)
    {
        try
        {
            unsafe
            {
                fixed (byte* p = fontData)
                {
                    ImGui.GetIO().Fonts.AddFontFromMemoryTTF((IntPtr)p, dataSize, pixelSize);
                }
            }
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    internal static void LoadFontAwesomeIcons()
    {
        unsafe
        {
            var iconsConfig = ImGuiNative.ImFontConfig_ImFontConfig();
            iconsConfig->MergeMode = 1; // merge the glyph ranges into the default font
            iconsConfig->PixelSnapH = 1; // don't try to render on partial pixels
            iconsConfig->FontDataOwnedByAtlas = 0; // the font atlas does not own this font data

            iconsConfig->GlyphMaxAdvanceX = float.MaxValue;
            iconsConfig->RasterizerMultiply = 1.0f;
            iconsConfig->OversampleH = 2;
            iconsConfig->OversampleV = 1;

            var iconRanges = new ushort[3];
            iconRanges[0] = FontAwesomeIcons.IconMin;
            iconRanges[1] = FontAwesomeIcons.IconMax;
            iconRanges[2] = 0;

            // using var stream = typeof(rlImGui).Assembly.GetManifestResourceStream("CopperDevs.DearImGui.Renderer.Raylib.FontAwesomeData.txt");
            fixed (ushort* range = &iconRanges[0])
            {
                // this unmanaged memory must remain allocated for the entire run of rlImgui
                FontAwesomeIcons.IconFontRanges = Marshal.AllocHGlobal(6);
                Buffer.MemoryCopy(range, FontAwesomeIcons.IconFontRanges.ToPointer(), 6, 6);
                iconsConfig->GlyphRanges = (ushort*)FontAwesomeIcons.IconFontRanges.ToPointer();

                using var stream = typeof(CopperImGui).Assembly.GetManifestResourceStream("CopperDevs.DearImGui.Resources.FontAwesomeData.txt");
                using var reader = new StreamReader(stream!);
                var streamTextResult = reader.ReadToEnd();

                var fontDataBuffer = Convert.FromBase64String(streamTextResult);

                fixed (byte* buffer = fontDataBuffer)
                {
                    ImGui.GetIO().Fonts.AddFontFromMemoryTTF(new IntPtr(buffer), fontDataBuffer.Length, 11, iconsConfig, FontAwesomeIcons.IconFontRanges);
                }
            }

            ImGuiNative.ImFontConfig_destroy(iconsConfig);
        }
    }

    internal static void UnloadFontAwesomeIcons()
    {
        if (FontAwesomeIcons.IconFontRanges != IntPtr.Zero)
            Marshal.FreeHGlobal(FontAwesomeIcons.IconFontRanges);

        FontAwesomeIcons.IconFontRanges = IntPtr.Zero;
    }
}