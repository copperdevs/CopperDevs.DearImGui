# Coppers DearImGui

> Custom layering around [DearImGui](https://github.com/ocornut/imgui)

## Info

Personally direct DearImGui didn't feel the best in C#, so I made this package to (subjectively) improve upon it.

## Installation

```bash
dotnet add package CopperDevs.DearImGui --version 1.4.2
```

Additionally, you need to install a relevant renderer package. There is currently two pre-made packages available.

*Note - When using raylib there's some extra steps that can be found [here](https://github.com/copperdevs/CopperDevs.DearImGui/blob/master/src/Renderers/Raylib/ReadMe.md)*

```bash
dotnet add package CopperDevs.DearImGui.Renderer.Raylib --version 2.0.2
```

```bash
dotnet add package CopperDevs.DearImGui.Renderer.OpenGl.SilkNet --version 1.0.8
```

## To Do

- [ ] Documentation
- [ ] Examples
    - [X] Raylib
        - [X] [Raylib-CSharp](https://github.com/MrScautHD/Raylib-CSharp)
        - [X] [Raylib-Cs](https://github.com/chrisdill/raylib-cs)
        - [X] [Raylib-CSharp-Vinculum](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum)
    - [ ] OpenGl ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] Vulkan ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] OpenGl ([OpenTk](https://github.com/opentk/opentk)) ***Maybe***
    - [X] [Sparkle](https://github.com/MrScautHD/Sparkle/tree/main)
- Pre-made renderer packages
    - [X] Raylib
        - [X] [Raylib-CSharp](https://github.com/MrScautHD/Raylib-CSharp)
        - [X] [Raylib-Cs](https://github.com/chrisdill/raylib-cs)
        - [X] [Raylib-CSharp-Vinculum](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum)
    - [X] OpenGl ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] Vulkan ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] OpenGl ([OpenTk](https://github.com/opentk/opentk)) ***Maybe***

## Examples

- [OpenGl](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/CopperDevs.DearImGui.Renderer.OpenGl.SilkNet) ([Silk.NET](https://github.com/dotnet/Silk.NET))
- [Raylib](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/src/Renderers/Raylib/CopperDevs.DearImGui.Renderer.Raylib.Raylib-CSharp) ([Raylib-CSharp](https://github.com/MrScautHD/Raylib-CSharp))
- [Raylib](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/src/Renderers/Raylib/CopperDevs.DearImGui.Renderer.Raylib.Raylib-cs) ([Raylib-Cs](https://github.com/chrisdill/raylib-cs))
- [Raylib](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/src/Renderers/Raylib/CopperDevs.DearImGui.Renderer.Raylib.Raylib-CSharp-Vinculum) ([Raylib-CSharp-Vinculum](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum))