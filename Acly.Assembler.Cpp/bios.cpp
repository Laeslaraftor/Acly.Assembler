// bios.cpp
extern "C" {
    // Очистка экрана (прерывание BIOS 0x10, функция 0x06)
    void ClearScreen() {
        __asm {
            mov ax, 0x0600  // AH=0x06 (scroll up), AL=0x00 (весь экран)
            mov bh, 0x07    // Атрибут (серый на чёрном)
            xor cx, cx      // Верхний левый угол (0,0)
            mov dx, 0x184F  // Нижний правый угол (24,79)
            int 0x10
        }
    }

    // Вывод символа (прерывание BIOS 0x10, функция 0x0E)
    void PrintChar(char c) {
        __asm {
            mov ah, 0x0E  // Функция вывода символа
            mov al, [c]   // Символ
            mov bh, 0x00  // Номер страницы
            int 0x10
        }
    }
}