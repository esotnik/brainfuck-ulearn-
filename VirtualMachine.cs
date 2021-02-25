using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public VirtualMachine(string program, int memorySize)
		{
			if (memorySize == 0)
				this.Memory = new byte[30000];
			else
				this.Memory = new byte[memorySize];
			this.MemoryPointer = 0;
			this.InstructionPointer = 0;
			this.Instructions = program;
			ActionList = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			ActionList.Add(symbol, execute);
		}

		public string Instructions { get; }
		public Dictionary<char, Action<IVirtualMachine>> ActionList { get; set; } 
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				var c = Instructions[InstructionPointer];
				if (ActionList.ContainsKey(c))
					ActionList[c](this);
			}
		}
	}
}