# Interfaz POS para PlatosView - Resumen de Implementación

## Descripción General
Se implementó una interfaz tipo POS (Point of Sale) para la vista de platos, reemplazando la tabla tradicional DataGrid con tarjetas visuales que muestran:
- Imagen del plato
- ID del plato
- Nombre del plato
- Precio formateado
- Estado de disponibilidad

## Cambios Realizados

### 1. **Nuevo Converter: PlatoImageConverter.cs**
**Ubicación:** `Utilities/PlatoImageConverter.cs`

- Mapea automáticamente el nombre del plato a su imagen correspondiente
- Lógica de mapeo:
  - "agua" → `agua.png`
  - "capuchino" → `capuchino.png`
  - "cruasan" → `cruasan.png`
  - "espresso" o "expresso" → `expresso.png`
  - "torta de chocolate", "tortadechocolate", "torta chocolate" → `tortadechocolate.png`
  - Otros → `comida.png` (imagen genérica por defecto)

### 2. **Actualización de App.xaml**
- Agregado namespace: `xmlns:utilities="clr-namespace:RestaurantApp.Utilities"`
- Registrado converter: `<utilities:PlatoImageConverter x:Key="PlatoImageConverter"/>`
- Agregado estilo: `PlatoCardStyle` para las tarjetas de platos
  - Bordes redondeados (12px)
  - Bordes color AccentColor (2px)
  - Sombra DropShadow sutil
  - Fondo blanco

### 3. **Rediseño de PlatosView.xaml**
**Cambios principales:**
- Reemplazado DataGrid con ItemsControl usando WrapPanel
- Las tarjetas de platos ahora son clicables
- Cada tarjeta contiene:
  - Imagen centrada (140px de altura)
  - ID del plato (texto pequeño)
  - Nombre del plato (truncado con ellipsis)
  - Precio en color PrimaryColor
  - Botón de disponibilidad con color dinámico:
    - Verde (#4CAF50) si está disponible
    - Rojo (#F44336) si no está disponible

### 4. **Actualización del ViewModel: PlatosViewModel.cs**
- Agregado nuevo comando: `SelectPlatoCommand`
- Nueva función: `SelectPlato(object? parameter)`
- Permite seleccionar un plato clickeando en su tarjeta
- La selección carga automáticamente los datos en el formulario de gestión

### 5. **Configuración de Recursos (.csproj)**
- Agregado: `<Resource Include="Resources/**/*.png" />`
- Asegura que todas las imágenes PNG sean empotradas como recursos

## Estructura de Tarjeta de Plato

```
┌─────────────────────────────┐
│   [IMAGEN DEL PLATO]        │ (140px)
│                             │
├─────────────────────────────┤
│ ID: 1                       │ (10px, gris)
├─────────────────────────────┤
│ Nombre del Plato            │ (14px, semibold)
│ (puede ocupar 2 líneas)     │
├─────────────────────────────┤
│ $12.00                      │ (16px, color primario)
├─────────────────────────────┤
│ [Disponible]                │ (Verde/Rojo según estado)
└─────────────────────────────┘
```

## Dimensiones
- Ancho de tarjeta: 200px
- Alto de tarjeta: 280px
- Espaciado entre tarjetas: 8px
- Imagen: 140px altura

## Tipografías Utilizadas
- ID: PoppinsRegular, 10px
- Nombre: PopinsSemiBold, 14px
- Precio: PopinsSemiBold, 16px
- Botón: PopinsSemiBold, 12px

## Interacción
1. Al hacer clic en una tarjeta, el plato se selecciona
2. Los campos del formulario (lado derecho) se rellenan automáticamente
3. Se puede editar, eliminar o cambiar disponibilidad
4. El botón "Limpiar" vacía los campos y deselecciona el plato

## Imágenes Requeridas
Ubicación: `Resources/`
- agua.png
- capuchino.png
- cruasan.png
- expresso.png (también acepta "espresso")
- tortadechocolate.png
- comida.png (imagen genérica para otros platos)

Todas las imágenes están configuradas como recursos empotrados en el proyecto.

## Notas Técnicas
- Utiliza WrapPanel para disposición responsiva de tarjetas
- Implementa DataTrigger para cambiar el color del botón según disponibilidad
- Las imágenes se cargan mediante binding del nombre del plato
- Compatible con .NET 9 Windows Desktop
- Fuente Poppins integrada (Regular y SemiBold)
