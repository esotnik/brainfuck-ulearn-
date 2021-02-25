using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            // ¬ывести байт пам€ти, на который указывает указатель, преобразовав в символ согласно ASCII
            vm.RegisterCommand('.', b => { write(Convert.ToChar(b.Memory[b.MemoryPointer])); });
            // ”величить байт пам€ти, на который указывает указатель
            vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer] = Convert.ToByte((b.Memory[b.MemoryPointer] + 1) & 0xFF); });
            // ”меньшить байт пам€ти, на который указывает указатель
            vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer] = Convert.ToByte((b.Memory[b.MemoryPointer] + 255) & 0xFF); });
            // ¬вести символ и сохранить его ASCII-код в байт пам€ти, на который указывает указатель
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = Convert.ToByte(read()); });
            // —двинуть указатель пам€ти вправо на 1 байт
            vm.RegisterCommand('>', b => { b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length; });
            // —двинуть указатель пам€ти влево на 1 байт
            vm.RegisterCommand('<', b =>
            {
                b.MemoryPointer = (b.MemoryPointer + b.Memory.Length - 1) % b.Memory.Length;
            });
            // сохранить ASCII-код этого символа в байт пам€ти, на который указывает указатель
            const string literals = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (var ix = 0; ix < literals.Length; ix++)
            {
                var literal = literals[ix];
                vm.RegisterCommand(literal, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(literal); });
            }
        }
	}
}