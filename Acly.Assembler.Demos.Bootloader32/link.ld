ENTRY(_start)

SECTIONS
{
    . = 0x100; /* Начальная точка загрузки (1MB) */

    .text : {
        *(.text)
    }

    .rodata : {
        *(.rodata*)
    }

    .data : {
        *(.data)
    }

    .bss : {
        *(.bss)
    }

    /DISCARD/ : {
        *(.comment)
    }
}