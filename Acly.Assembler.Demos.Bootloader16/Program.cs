using Acly.Assembler.Demos.Bootloader16;
using System.Diagnostics;

async Task Execute(string command)
{
    string args = $"/c \"{command}\"";
    var process = Process.Start(new ProcessStartInfo("cmd.exe", args));
    
    if (process != null)
    {
        await process.WaitForExitAsync();
    }
}

string outputFolder = Path.Combine(Environment.CurrentDirectory, "output");
string assemblyFile = Path.Combine(outputFolder, "boot.asm");

if (!Directory.Exists(outputFolder))
{
    Directory.CreateDirectory(outputFolder);
}

await Bootloader.Create(assemblyFile);

await Execute($"nasm -f bin \"{assemblyFile}\" -o output/boot.bin");
await Execute("ddrelease64 if=/dev/zero of=output/hdd.img count=32768");
await Execute("ddrelease64 if=output/boot.bin of=output/hdd.img bs=512 conv=notrunc");
await Execute("qemu-system-x86_64 -drive format=raw,file=output/hdd.img");