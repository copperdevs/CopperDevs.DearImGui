# Coppers DearImGui

> Custom layering around [DearImGui](https://github.com/ocornut/imgui)

## Info

Personally direct DearImGui didn't feel the best in C#, so I made this package to (subjectively) improve upon it.

## Installation

```
dotnet add package CopperDevs.DearImGui --version 1.2.0
```

Additionally, you need to install a relevant renderer package. There is currently two pre-made packages available.

```
dotnet add package CopperDevs.DearImGui.Renderer.Raylib --version 1.1.1
```

```
dotnet add package CopperDevs.DearImGui.Renderer.OpenGl.SilkNet --version 1.0.1
```

## To Do

- [ ] Documentation
- [ ] Better examples
    - [X] Raylib ([Raylib-CSharp](https://github.com/MrScautHD/Raylib-CSharp))
    - [ ] Raylib ([raylib-cs](https://github.com/chrisdill/raylib-cs)) ***Maybe***
    - [ ] OpenGl ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] Vulkan ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] OpenGl ([OpenTk](https://github.com/opentk/opentk)) ***Maybe***
- Pre-made renderer packages
    - [X] Raylib ([Raylib-CSharp](https://github.com/MrScautHD/Raylib-CSharp))
    - [ ] Raylib ([raylib-cs](https://github.com/chrisdill/raylib-cs)) ***Maybe***
    - [X] OpenGl ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] Vulkan ([Silk.NET](https://github.com/dotnet/Silk.NET))
    - [ ] OpenGl ([OpenTk](https://github.com/opentk/opentk)) ***Maybe***
- [ ] [Changable backends](https://github.com/copperdevs/CopperDevs.DearImGui/tree/multi-backends) **Maybe**
    - [ ] [ImGui.NET](https://github.com/ImGuiNET/ImGui.NET)
    - [ ] [Hexa.Net.ImGui](https://www.nuget.org/packages/Hexa.NET.ImGui/)

## Examples

- [Silk.NET OpenGl](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/CopperDevs.DearImGui.Renderer.OpenGl.SilkNet)
- [Raylib-CSharp](https://github.com/copperdevs/CopperDevs.DearImGui/tree/master/CopperDevs.DearImGui.Renderer.Raylib)