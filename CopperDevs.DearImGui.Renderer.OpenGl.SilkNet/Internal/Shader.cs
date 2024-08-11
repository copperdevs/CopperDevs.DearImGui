// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using Silk.NET.OpenGL;

namespace CopperDevs.DearImGui.Renderer.OpenGl.SilkNet.Internal
{
    internal struct UniformFieldInfo
    {
        public int Location;
        public string Name;
        public int Size;
        public UniformType Type;
    }

    internal class Shader
    {
        public uint Program { get; private set; }
        private readonly Dictionary<string, int> _uniformToLocation = new();
        private readonly Dictionary<string, int> _attribLocation = new();
        private bool _initialized = false;
        private GL _gl;
        private (ShaderType Type, string Path)[] _files;

        public Shader(GL gl, string vertexShader, string fragmentShader)
        {
            _gl = gl;
            _files = new[]
            {
                (ShaderType.VertexShader, vertexShader),
                (ShaderType.FragmentShader, fragmentShader),
            };
            Program = CreateProgram(_files);
        }

        public void UseShader()
        {
            _gl.UseProgram(Program);
        }

        public void Dispose()
        {
            if (_initialized)
            {
                _gl.DeleteProgram(Program);
                _initialized = false;
            }
        }

        public UniformFieldInfo[] GetUniforms()
        {
            _gl.GetProgram(Program, GLEnum.ActiveUniforms, out var uniformCount);

            var uniforms = new UniformFieldInfo[uniformCount];

            for (var i = 0; i < uniformCount; i++)
            {
                var name = _gl.GetActiveUniform(Program, (uint)i, out var size, out var type);

                UniformFieldInfo fieldInfo;
                fieldInfo.Location = GetUniformLocation(name);
                fieldInfo.Name = name;
                fieldInfo.Size = size;
                fieldInfo.Type = type;

                uniforms[i] = fieldInfo;
            }

            return uniforms;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUniformLocation(string uniform)
        {
            if (_uniformToLocation.TryGetValue(uniform, out var location)) 
                return location;
            
            location = _gl.GetUniformLocation(Program, uniform);
            _uniformToLocation.Add(uniform, location);

            if (location == -1)
            {
                Debug.Print($"The uniform '{uniform}' does not exist in the shader!");
            }

            return location;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetAttribLocation(string attrib)
        {
            if (_attribLocation.TryGetValue(attrib, out var location)) 
                return location;
            
            location = _gl.GetAttribLocation(Program, attrib);
            _attribLocation.Add(attrib, location);

            if (location == -1)
            {
                Debug.Print($"The attrib '{attrib}' does not exist in the shader!");
            }

            return location;
        }

        private uint CreateProgram(params (ShaderType Type, string source)[] shaderPaths)
        {
            var program = _gl.CreateProgram();

            Span<uint> shaders = stackalloc uint[shaderPaths.Length];
            for (var i = 0; i < shaderPaths.Length; i++)
            {
                shaders[i] = CompileShader(shaderPaths[i].Type, shaderPaths[i].source);
            }

            foreach (var shader in shaders)
                _gl.AttachShader(program, shader);

            _gl.LinkProgram(program);

            _gl.GetProgram(program, GLEnum.LinkStatus, out var success);
            if (success == 0)
            {
                var info = _gl.GetProgramInfoLog(program);
                Debug.WriteLine($"GL.LinkProgram had info log:\n{info}");
            }

            foreach (var shader in shaders)
            {
                _gl.DetachShader(program, shader);
                _gl.DeleteShader(shader);
            }

            _initialized = true;

            return program;
        }

        private uint CompileShader(ShaderType type, string source)
        {
            var shader = _gl.CreateShader(type);
            _gl.ShaderSource(shader, source);
            _gl.CompileShader(shader);

            _gl.GetShader(shader, ShaderParameterName.CompileStatus, out var success);
            
            if (success != 0) 
                return shader;
            
            var info = _gl.GetShaderInfoLog(shader);
            Debug.WriteLine($"GL.CompileShader for shader [{type}] had info log:\n{info}");

            return shader;
        }
    }
}