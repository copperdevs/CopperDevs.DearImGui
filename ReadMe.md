# Coppers DearImGui

> Custom layering around [DearImGui](https://github.com/ocornut/imgui)

## Info

Personally direct DearImGui didn't feel the best in C#, so I made this package to (subjectively) improve upon it.

## Installation

```
wdotnet add package CopperDevs.DearImGui --version 1.2.2
```

Additionally, you need to install a relevant renderer package. There is currently two pre-made packages available.

*Note - When using raylib there's some extra steps that can be found [here](https://github.com/copperdevs/CopperDevs.DearImGui/blob/master/src/Renderers/Raylib/ReadMe.md)*

```bash
dotnet add package CopperDevs.DearImGui.Renderer.Raylib --version 2.0.0
```

```bash
dotnet add package CopperDevs.DearImGui.Renderer.OpenGl.SilkNet --version 1.0.4
```

## To Do

- [ ] Documentation
- [ ] Examples
    - [X] Raylib
        - [X] Raylib-cs
        - [X] Raylib-CSharp
        - [X] Raylib-CSharp-Vinculum
    - [ ] OpenGl ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] Vulkan ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] OpenGl ([OpenTk](https://github.com/opentk/opentk)) ***Maybe***
    - [X] [Sparkle](https://github.com/MrScautHD/Sparkle/tree/main)
- Pre-made renderer packages
    - [X] Raylib
        - [X] Raylib-cs
        - [X] Raylib-CSharp
        - [X] Raylib-CSharp-Vinculum
    - [X] OpenGl ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] Vulkan ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] OpenGl ([OpenTk](https://github.com/opentk/opentk)) ***Maybe***
- [ ] [Changable backends](https://github.com/copperdevs/CopperDevs.DearImGui/tree/multi-backends) **Maybe**
    - [ ] [ImGui.NET](https://github.com/ImGuiNET/ImGui.NET)
    - [ ] [Hexa.Net.ImGui](https://www.nuget.org/packages/Hexa.NET.ImGui/)

## Examples

- [OpenGl](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/CopperDevs.DearImGui.Renderer.OpenGl.SilkNet) ([Silk.NET](https://github.com/dotnet/Silk.NET))
- [Raylib](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/CopperDevs.DearImGui.Renderer.Raylib) ([Raylib-CSharp](https://github.com/MrScautHD/Raylib-CSharp))