using Acly.Assembler.Demos.Bootloader32;
using System.Diagnostics;

static async Task Execute(string command)
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
string kernelFile = Path.Combine(outputFolder, "kernel.asm");

if (!Directory.Exists(outputFolder))
{
    Directory.CreateDirectory(outputFolder);
}

await Bootloader16.Create(assemblyFile);
await Bootloader16Step2.Create(kernelFile);

await Execute($"nasm -f bin \"{assemblyFile}\" -o output/boot.bin");
await Execute($"nasm -f bin \"{kernelFile}\" -o output/kernelRaw.bin");
//await Execute("ld -m i386pe -T link.ld --image-base=0x100 -o output/kernel.pe output/kernelRaw.bin");
//await Execute("objcopy -O binary output/kernel.pe output/kernel.bin");
await Execute("ddrelease64 if=/dev/zero of=output/hdd.img count=32768");
await Execute("ddrelease64 if=output/boot.bin of=output/hdd.img bs=512 conv=notrunc");
await Execute("ddrelease64 if=output/kernelRaw.bin of=output/hdd.img seek=1 conv=notrunc");
await Execute("qemu-system-x86_64 -drive format=raw,file=output/hdd.img -d int -no-reboot -vga vmware");