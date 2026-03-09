# 📺 Referencia Visual - Interfaz POS con Asignación Automática

## Pantalla Completa de Platos

```
╔═══════════════════════════════════════════════════════════════════════════════╗
║                              RESTAURANTAPP                                    ║
║                                                                               ║
║  Platos | Mesas | Pedidos | Facturas | Reportes                             ║
╠═══════════════════════════════════════════════════════════════════════════════╣
║                                                                               ║
║  ┌──────────────────────────────────────────────┐  ┌──────────────────────┐ ║
║  │ 📋 Lista de Platos                           │  │ Gestión de Plato     │ ║
║  ├──────────────────────────────────────────────┤  ├──────────────────────┤ ║
║  │                                              │  │ Vista Previa:        │ ║
║  │  ┌──────────┐  ┌──────────┐  ┌──────────┐  │  │ ┌──────────────────┐ │ ║
║  │  │          │  │          │  │          │  │  │ │                  │ │ ║
║  │  │   💧    │  │   ☕    │  │   🍰    │  │  │ │   ☕ CAPUCHINO  │ │ ║
║  │  │          │  │          │  │          │  │  │ │                  │ ║
║  │  │  agua    │  │ capuchino│  │   torta  │  │  │ └──────────────────┘ │ ║
║  │  │   ID:1   │  │   ID:2   │  │   ID:3   │  │  │                      │ ║
║  │  │  $5.00   │  │ $12.00   │  │ $12.00   │  │  │ Nombre:              │ ║
║  │  │[Disponible]│ │[Disponible]│ │[Disponible]│  │ [capuchino_______] │ ║
║  │  │          │  │          │  │          │  │  │                      │ ║
║  │  └──────────┘  └──────────┘  └──────────┘  │  │ Precio:              │ ║
║  │                                              │  │ [12000__________]  │ ║
║  │  ┌──────────┐  ┌──────────┐  ┌──────────┐  │  │                      │ ║
║  │  │          │  │          │  │          │  │  │ [Registrar]          │ ║
║  │  │   🥐    │  │   🍽️    │  │  ???     │  │  │ [Eliminar]           │ ║
║  │  │          │  │          │  │          │  │  │ [Cambiar Disponib]   │ ║
║  │  │ cruasan  │  │  nueva   │  │  ejemplo │  │  │ [Limpiar]            │ ║
║  │  │   ID:4   │  │   ID:5   │  │   ID:6   │  │  │                      │ ║
║  │  │  $3.50   │  │  $8.00   │  │ $15.00   │  │  │                      │ ║
║  │  │[Disponible]│ │[Disponible]│ │[Disponible]│  │                      │ ║
║  │  │          │  │          │  │          │  │  │ Mensaje:             │ ║
║  │  └──────────┘  └──────────┘  └──────────┘  │  │ Plato actualizado... │ ║
║  │                                              │  │                      │ ║
║  └──────────────────────────────────────────────┘  └──────────────────────┘ ║
║                                                                               ║
╚═══════════════════════════════════════════════════════════════════════════════╝
```

## Desglose Visual

### 1. Panel Izquierdo: Lista de Platos (POS Grid)

Muestra todas las comidas en formato de tarjetas. Cada tarjeta tiene:

```
┌──────────────────────┐
│   [IMAGEN 140px]     │  ← Asignada automáticamente por nombre
├──────────────────────┤
│ ID: 2                │  ← Identificador único
├──────────────────────┤
│ capuchino            │  ← Nombre del plato
├──────────────────────┤
│ $12.00               │  ← Precio formateado
├──────────────────────┤
│ [Disponible]         │  ← Verde si está disponible
└──────────────────────┘    Rojo si NO está disponible
```

### 2. Panel Derecho: Formulario de Gestión

#### 2.1 Vista Previa (NUEVA FUNCIONALIDAD)

```
Vista Previa:
┌──────────────────────────────────┐
│                                  │
│        [IMAGEN AQUÍ 🖼️]         │  ← Se actualiza EN TIEMPO REAL
│                                  │     mientras escribes el nombre
│                                  │
└──────────────────────────────────┘
```

#### 2.2 Campos de Entrada

```
Nombre:
[___________________________]  ← Escribe aquí (ej: "capuchino")
                                La imagen en preview se actualiza
                                automáticamente

Precio:
[___________________________]  ← Escribe el precio (ej: "12000")
```

#### 2.3 Botones de Acción

```
[Registrar]           ← Guarda el plato. La imagen se asigna automáticamente
[Eliminar]            ← Elimina el plato seleccionado
[Cambiar Disponibilidad]  ← Activa/desactiva disponibilidad
[Limpiar]             ← Vacía los campos
```

---

## 🎬 Interacción Paso a Paso

### Escenario: Crear un Nuevo Capuchino

#### Paso 1: Escribir el Nombre
```
Usuario escribe: "c" en el campo Nombre
├─ Preview muestra: comida.png (genérica)
│
Usuario continúa: "ca"
├─ Preview muestra: comida.png (genérica)
│
Usuario continúa: "cap"
├─ Preview muestra: comida.png (genérica)
│
Usuario continúa: "capu"
├─ Preview muestra: comida.png (genérica)
│
Usuario continúa: "capuchino"
├─ Preview muestra: capuchino.png ☕ ✅ ¡COINCIDENCIA!
```

#### Paso 2: Escribir el Precio
```
Usuario escribe: "12000"
├─ Preview sigue mostrando: capuchino.png ☕
└─ Todo listo para registrar
```

#### Paso 3: Registrar
```
Usuario hace clic en "Registrar"
├─ Se guarda en BD
├─ Se limpia el formulario
├─ Se recarga la lista
└─ En la lista aparece nueva tarjeta:
   
   ┌────────────────────┐
   │  ☕ CAPUCHINO      │
   │  ID: 2             │
   │  capuchino         │
   │  $12000.00         │
   │  [Disponible]      │
   └────────────────────┘
```

---

## 🖱️ Interacciones del Usuario

### Al escribir un nombre:
```
"agua"        ➜ Preview: agua.png         💧
"AGUA"        ➜ Preview: agua.png         💧 (mayúsculas OK)
"capuchino"   ➜ Preview: capuchino.png    ☕
"espresso"    ➜ Preview: expresso.png     ☕
"pizza"       ➜ Preview: comida.png       🍽️ (por defecto)
"xxxxx"       ➜ Preview: comida.png       🍽️ (por defecto)
```

### Al hacer clic en una tarjeta:
```
Usuario hace clic en tarjeta "capuchino"
├─ El campo Nombre se rellena con "capuchino"
├─ El campo Precio se rellena con "12000"
├─ Preview muestra capuchino.png
├─ Botones "Eliminar" y "Cambiar Disponibilidad" se activan
└─ Usuario puede editar o eliminar
```

---

## 🎨 Colores y Estilos

### Tarjetas de Platos:
- **Fondo:** Blanco
- **Borde:** Dorado (AccentColor: #FFD4A373)
- **Grosor borde:** 2px
- **Esquinas:** Redondeadas (12px)
- **Sombra:** Suave (BlurRadius: 8)

### Botón de Disponibilidad:
- **Si disponible:** Verde (#4CAF50) + Texto "Disponible"
- **Si no disponible:** Rojo (#F44336) + Texto "No Disponible"
- **Letra:** Poppins SemiBold, 12px

### Área de Precios:
- **Color:** Marrón primario (#6F4E37)
- **Tamaño:** 16px, SemiBold

---

## 📊 Información Mostrada por Tarjeta

| Elemento | Tamaño | Familia | Color |
|---|---|---|---|
| ID | 10px | Poppins Regular | Gris #999999 |
| Nombre | 14px | Poppins SemiBold | Texto principal #2D2D2D |
| Precio | 16px | Poppins SemiBold | Marrón primario #6F4E37 |
| Imagen | 140px altura | - | - |
| Botón | 12px | Poppins SemiBold | Verde/Rojo |

---

## 📱 Responsividad

- **Tarjetas por fila:** Depende del ancho de ventana (WrapPanel)
- **Ancho de tarjeta:** 200px (fijo)
- **Alto de tarjeta:** 280px (fijo)
- **Espaciado:** 8px entre tarjetas
- **Scroll vertical:** Activo en la lista

---

## ✨ Características Visuales Principales

### 1. Preview en Tiempo Real
```
┌─ Label: "Vista Previa:"
├─ Imagen con Border:
│  ├─ Fondo: Beige (#F5F1EC)
│  ├─ Borde: Dorado (AccentColor)
│  ├─ Esquinas: Redondeadas (8px)
│  └─ Altura: 120px
└─ Se actualiza mientras escribes
```

### 2. Tarjetas en Grid POS
```
┌─ Layout: WrapPanel
├─ Cada tarjeta:
│  ├─ Imagen 140px
│  ├─ ID pequeño
│  ├─ Nombre truncable
│  ├─ Precio destacado
│  └─ Botón estado (dinámico)
└─ Clic = Selecciona plato
```

### 3. Formulario Limpio
```
┌─ ScrollViewer (si hay mucho contenido)
├─ Vista Previa arriba
├─ Campos de entrada
├─ Botones de acción
└─ Mensajes de estado
```

---

## 🎯 Flujo Visual Completo

```
┌─────────────────────────────────┐
│ Usuario abre app - Panel Platos │
└──────────┬──────────────────────┘
           │
           ├─► Lista vacía o con platos previos
           │   (lado izquierdo con tarjetas)
           │
           └─► Formulario listo
               (lado derecho con preview vacío)
                    │
                    ├─► Usuario escribe "agua"
                    │   └─► Preview: agua.png
                    │
                    ├─► Usuario escribe precio "5000"
                    │   └─► Preview sigue siendo agua.png
                    │
                    ├─► Usuario hace clic "Registrar"
                    │   └─► Se guarda, se recarga
                    │
                    └─► En la lista aparece:
                        ┌──────────────┐
                        │  💧 AGUA     │
                        │  $5000.00    │
                        │ [Disponible] │
                        └──────────────┘
```

---

## 💡 Notas Importantes

1. **La imagen se asigna automáticamente** - No requiere selección manual
2. **Se ve en preview antes de registrar** - Puedes verificar antes de guardar
3. **Las mayúsculas no importan** - `AGUA`, `agua`, `Agua` funcionan igual
4. **Las excepciones usan comida.png** - Si no coincide exacto, usa genérico
5. **Totalmente funcional** - Ya compiló y está listo para usar

¡Tu aplicación ahora tiene un sistema de asignación visual completo y automático! 🎉
