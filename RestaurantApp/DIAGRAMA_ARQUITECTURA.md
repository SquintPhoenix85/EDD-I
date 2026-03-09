# 📊 Diagrama de Arquitectura - Sistema de Asignación de Imágenes

## Arquitectura General

```
┌─────────────────────────────────────────────────────────────────────────┐
│                          RESTAURANTAPP                                  │
│                                                                         │
│  ┌────────────────────────────────────────────────────────────────┐   │
│  │                       VISTA XAML                               │   │
│  │                    PlatosView.xaml                             │   │
│  │                                                                │   │
│  │  ┌──────────────────────┐      ┌──────────────────────────┐  │   │
│  │  │  Lista de Platos     │      │  Gestión de Plato        │  │   │
│  │  │  (WrapPanel Grid)    │      │  (Formulario)            │  │   │
│  │  │                      │      │                          │  │   │
│  │  │ ┌────────────────┐   │      │ ┌──────────────────────┐ │  │   │
│  │  │ │ ItemsControl   │   │      │ │ Vista Previa:        │ │  │   │
│  │  │ │ Binding: Platos│   │      │ │ [Image]              │ │  │   │
│  │  │ │                │   │      │ │ Binding: Nombre      │ │  │   │
│  │  │ └─────┬──────────┘   │      │ │ Converter: ──────┐   │ │  │   │
│  │  │       │              │      │ └────────────────┼──┐  │  │   │
│  │  │       ▼              │      │                  │  │  │  │   │
│  │  │ ┌────────────────┐   │      │ ┌──────────────┐ │  │  │  │   │
│  │  │ │ DataTemplate   │   │      │ │ TextBox Name│ │  │  │  │   │
│  │  │ │ (Tarjeta)      │   │      │ │ Binding:───┼┼──┘  │  │   │
│  │  │ │                │   │      │ │     Nombre │ │     │  │   │
│  │  │ │ [Image]        │───┼──────┼─┤ Converter: │ │     │  │   │
│  │  │ │ Binding: Nombre│   │      │ │ Imagen     │ │     │  │   │
│  │  │ │ Converter:─────┼──┼───┐   │ │            │ │     │  │   │
│  │  │ │ PlatoImage     │   │   │   │ └────────────┘ │     │  │   │
│  │  │ │                │   │   │   │                │     │  │   │
│  │  │ │ ID, Nombre,    │   │   │   │ TextBox Precio│     │  │   │
│  │  │ │ Precio         │   │   │   │ [Registrar]   │     │  │   │
│  │  │ │ Botón Estado   │   │   │   │ [Eliminar]    │     │  │   │
│  │  │ └────────────────┘   │   │   │ [Limpiar]     │     │  │   │
│  │  │                      │   │   │                │     │  │   │
│  │  └──────────────────────┘   │   └────────────────┘     │  │   │
│  │                             │                          │  │   │
│  └─────────────────────────────┼──────────────────────────┘  │   │
│                                │                             │   │
│        CONVERTER/BINDING        │                             │   │
│                                │                             │   │
│  ┌────────────────────────────┴──────────────────────────┐  │   │
│  │                                                       │  │   │
│  │  ┌──────────────────────────────────────────────┐   │  │   │
│  │  │  PlatoImageConverter                         │   │  │   │
│  │  │  (Utilities/PlatoImageConverter.cs)          │   │  │   │
│  │  │                                              │   │  │   │
│  │  │  public object Convert(object? value, ...) │   │  │   │
│  │  │  {                                          │   │  │   │
│  │  │    var nombreLower = value.ToLower().Trim(); │   │  │   │
│  │  │                                              │   │  │   │
│  │  │    return nombreLower switch                │   │  │   │
│  │  │    {                                        │   │  │   │
│  │  │      "agua" => agua.png,                    │   │  │   │
│  │  │      "capuchino" => capuchino.png,          │   │  │   │
│  │  │      "cruasan" => cruasan.png,              │   │  │   │
│  │  │      "espresso" or "expresso" =>            │   │  │   │
│  │  │        expresso.png,                        │   │  │   │
│  │  │      "torta de chocolate" or ... =>         │   │  │   │
│  │  │        tortadechocolate.png,                │   │  │   │
│  │  │      _ => comida.png                        │   │  │   │
│  │  │    };                                       │   │  │   │
│  │  │  }                                          │   │  │   │
│  │  │                                              │   │  │   │
│  │  └──────────────────────────────────────────────┘   │  │   │
│  │                                                       │  │   │
│  └───────────────────────────────────────────────────────┘  │   │
│                                                             │   │
│  ┌────────────────────────────────────────────────────┐    │   │
│  │          VIEWMODEL                                │    │   │
│  │   PlatosViewModel : BaseViewModel                 │    │   │
│  │                                                    │    │   │
│  │  ObservableCollection<Plato> Platos              │    │   │
│  │  string Nombre { get; set; }                      │    │   │
│  │  string Precio { get; set; }                      │    │   │
│  │  ICommand GuardarCommand                          │    │   │
│  │  ICommand SelectPlatoCommand                      │    │   │
│  │                                                    │    │   │
│  │  OnPropertyChanged() ← Notifica cambios al Binding│   │   │
│  │                                                    │    │   │
│  └────────────────────────────────────────────────────┘    │   │
│                                                             │   │
│  ┌────────────────────────────────────────────────────┐    │   │
│  │           MODELO                                   │    │   │
│  │   Plato : IEntity                                 │    │   │
│  │                                                    │    │   │
│  │   int Id { get; set; }                            │    │   │
│  │   string Nombre { get; set; }                     │    │   │
│  │   decimal Precio { get; set; }                    │    │   │
│  │   bool Disponible { get; set; }                   │    │   │
│  │                                                    │    │   │
│  └────────────────────────────────────────────────────┘    │   │
│                                                             │   │
│  ┌────────────────────────────────────────────────────┐    │   │
│  │            RECURSOS                               │    │   │
│  │                                                    │    │   │
│  │  /Resources/                                      │    │   │
│  │  ├─ agua.png          ◄────────────────────┐     │    │   │
│  │  ├─ capuchino.png     ◄────────────────────┼─────┼─┐  │   │
│  │  ├─ cruasan.png       ◄────────────────────┼─────┼─┼──┼─┐ │   │
│  │  ├─ expresso.png      ◄────────────────────┼─────┼─┼──┼─┤ │   │
│  │  ├─ tortadechocolate.png ◄────────────────┼─────┼─┼──┼─┤ │   │
│  │  └─ comida.png (por defecto) ◄────────────┘     │ │  │ │ │   │
│  │                                     ▲            │ │  │ │ │   │
│  │                                     │            │ │  │ │ │   │
│  └─────────────────────────────────────┼────────────┼─┼──┼─┤ │   │
│                                        │            │ │  │ │ │   │
└────────────────────────────────────────┼────────────┼─┼──┼─┤ │───┘
                                         │            │ │  │ │ │
                                    "pack://app"     │ │  │ │ │
                                    "lication:,,,/"  │ │  │ │ │
                                         │            │ │  │ │ │
                                         └────────────┴─┴──┴─┴─┘
                                         (Devuelve ruta de imagen)
```

---

## Flujo de Datos

```
┌─────────────────────┐
│  Usuario escribe    │
│  "capuchino" en     │
│  campo TextBox      │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  Binding captura el cambio              │
│  UpdateSourceTrigger=PropertyChanged    │
│  Nombre = "capuchino"                   │
└──────────┬──────────────────────────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  Binding pasa el valor al Converter     │
│  Convert() recibe "capuchino"           │
└──────────┬──────────────────────────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  Converter procesa:                     │
│  1. ToLower() → "capuchino"            │
│  2. Trim()    → "capuchino"            │
│  3. switch { ... } → matching           │
└──────────┬──────────────────────────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  Converter devuelve:                    │
│  "pack://application:,,,/Resources/    │
│   capuchino.png"                        │
└──────────┬──────────────────────────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  Binding asigna resultado a:            │
│  <Image Source="{...}" />               │
└──────────┬──────────────────────────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  WPF carga la imagen desde recursos:    │
│  capuchino.png                          │
└──────────┬──────────────────────────────┘
           │
           ▼
┌─────────────────────────────────────────┐
│  ✅ Imagen aparece en preview           │
│     EN TIEMPO REAL                      │
└─────────────────────────────────────────┘
```

---

## Diagrama de Clases

```
┌─────────────────────────────────────────────────────────────┐
│                    <<interface>>                            │
│                  IValueConverter                            │
│  ─────────────────────────────────────────────────────────  │
│  + Convert(object?, Type, object?, CultureInfo): object    │
│  + ConvertBack(object?, Type, object?, CultureInfo): object│
└────────────────┬──────────────────────────────────────────┘
                 │
                 │ implements
                 │
┌─────────────────────────────────────────────────────────────┐
│            PlatoImageConverter                              │
│  ─────────────────────────────────────────────────────────  │
│  + Convert(object?, Type, object?, CultureInfo): string    │
│    └─ Recibe: nombre del plato                             │
│    └─ Devuelve: ruta de imagen (string)                    │
│    └─ Lógica: switch con mapeo de nombres                  │
│  + ConvertBack(...): NotImplementedException               │
└──────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                 <<model>>                                   │
│                   Plato                                     │
│  ─────────────────────────────────────────────────────────  │
│  - id: int                                                  │
│  - nombre: string                                           │
│  - precio: decimal                                          │
│  - disponible: bool                                         │
│  ─────────────────────────────────────────────────────────  │
│  + ToString(): string                                       │
│  + FromString(string): Plato                                │
└──────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│              <<viewmodel>>                                  │
│            PlatosViewModel                                  │
│  ─────────────────────────────────────────────────────────  │
│  - _service: PlatoService                                   │
│  - _selectedPlato: Plato?                                   │
│  - _nombre: string                                          │
│  - _precio: string                                          │
│  ─────────────────────────────────────────────────────────  │
│  + Platos: ObservableCollection<Plato>                      │
│  + Nombre: string { get; set; }                             │
│  + Precio: string { get; set; }                             │
│  + GuardarCommand: ICommand                                 │
│  + SelectPlatoCommand: ICommand                             │
│  ─────────────────────────────────────────────────────────  │
│  + SelectPlato(object?): void                               │
│  + CargarPlatos(): void                                     │
└──────────────────────────────────────────────────────────────┘
```

---

## Casos de Uso

```
┌─────────────────────────────────────────────────────────────────┐
│              CASOS DE USO - ASIGNACIÓN DE IMÁGENES              │
└─────────────────────────────────────────────────────────────────┘

1. REGISTRAR PLATO CON NOMBRE ESPECÍFICO
   ┌─────────────────┐
   │  Usuario        │
   └────────┬────────┘
            │
            ├─> Escribe "capuchino"
            │       │
            │       ▼
            │   Preview: capuchino.png ✅
            │
            ├─> Escribe precio "12000"
            │
            ├─> Clic "Registrar"
            │       │
            │       ▼
            │   Guarda en BD
            │
            └─> En lista: Tarjeta con capuchino.png ✅

2. REGISTRAR PLATO CON NOMBRE DESCONOCIDO
   ┌─────────────────┐
   │  Usuario        │
   └────────┬────────┘
            │
            ├─> Escribe "pizza"
            │       │
            │       ▼
            │   Preview: comida.png ✅
            │
            ├─> Escribe precio "8000"
            │
            ├─> Clic "Registrar"
            │       │
            │       ▼
            │   Guarda en BD
            │
            └─> En lista: Tarjeta con comida.png ✅

3. SELECCIONAR PLATO EXISTENTE
   ┌─────────────────┐
   │  Usuario        │
   └────────┬────────┘
            │
            ├─> Clic en tarjeta "agua"
            │       │
            │       ▼
            │   SelectPlatoCommand ejecuta
            │       │
            │       ▼
            │   SelectedPlato = agua
            │       │
            │       ▼
            │   Campos se rellenan:
            │   Nombre: "agua"
            │   Precio: "5.00"
            │   Preview: agua.png
            │       │
            │       ▼
            │   Usuario puede editar/eliminar
            │
            └─> Clic "Guardar" para actualizar

4. ESCRIBIR CON MAYÚSCULAS
   ┌─────────────────┐
   │  Usuario        │
   └────────┬────────┘
            │
            ├─> Escribe "AGUA"
            │       │
            │       ▼
            │   Converter.ToLower() → "agua"
            │       │
            │       ▼
            │   Preview: agua.png ✅
            │
            └─> Resultado igual a "agua" ✅
```

---

## Dependencias y Referencias

```
DIAGRAMA DE DEPENDENCIAS

        App.xaml
            │
            ├─ Registra: PlatoImageConverter
            │
            ├─ Define: Recursos visuales
            │           (Fuentes, colores, estilos)
            │
            └─ Carga: PresentationFramework.Fluent

        PlatosView.xaml
            │
            ├─ Referencia: PlatoImageConverter
            │
            ├─ Binding a: PlatosViewModel
            │
            └─ Usa: Estilos de App.xaml

        PlatosViewModel.cs
            │
            ├─ Usa: PlatoService
            │       └─ Accede a: BD (Platos)
            │
            ├─ Usa: RelayCommand
            │
            ├─ Implementa: BaseViewModel
            │             └─ OnPropertyChanged()
            │
            └─ Trabaja con: Modelo Plato

        PlatoImageConverter.cs
            │
            ├─ Implementa: IValueConverter
            │
            ├─ Referencia: Resources/*.png
            │
            └─ Sin dependencias externas
```

---

## Flujo de Binding y Conversion

```
SOURCE              BINDING              CONVERTER           TARGET
────────────────────────────────────────────────────────────────────

Plato.Nombre    ────────────────────►  PlatoImageConverter  ► Image.Source
"capuchino"     {Binding Nombre,       Convert()            pack://app...
                 Converter=...}        └─ "capuchino"       /capuchino.png
                                       └─ switch
                                       └─ return URL

PropertyChanged ────────────────────►  Notifica al Binding  ► Actualiza UI
(UpdateSourceTrigger                   └─ Dispara Converter
 =PropertyChanged)                      └─ Nueva URL
                                        └─ Recarga imagen
```

---

## Estados y Transiciones

```
┌─────────────────────────────────────────────────────────┐
│                  MÁQUINA DE ESTADOS                     │
└─────────────────────────────────────────────────────────┘

                    ┌─────────────┐
                    │   INICIO    │
                    │  (App load) │
                    └────────┬────┘
                             │
                             ▼
                    ┌─────────────┐
                    │  Cargar BD  │
                    │ (Platos)    │
                    └────────┬────┘
                             │
                             ▼
                    ┌──────────────────┐
                    │ Mostrar Lista    │
                    │ Converter aplica │
                    │ imagen a c/plato │
                    └────────┬─────────┘
                             │
                    ┌────────┴─────────┐
                    │                  │
                    ▼                  ▼
            ┌──────────────┐  ┌─────────────────┐
            │ Usuario      │  │ Usuario clic    │
            │ escribe      │  │ en tarjeta      │
            │ nombre       │  │                 │
            └───┬──────────┘  └────────┬────────┘
                │                     │
                │ Preview            │ SelectPlato
                │ actualiza          │ Command
                │ en tiempo          │
                │ real               │
                │                     │
                ▼                      ▼
         ┌──────────────────────────────────┐
         │ Plato seleccionado              │
         │ Nombre + Precio cargados        │
         │ Preview muestra imagen          │
         │ Botones de acción habilitados   │
         └───┬──────────────────┬──────────┘
             │                  │
      ┌──────┴──────┐      ┌────┴──────────┐
      │             │      │               │
      ▼             ▼      ▼               ▼
   Guardar      Eliminar Limpiar      Cambiar
   Comando      Comando  Comando      Disponibilidad
      │             │      │               │
      └────────┬────┴──────┴──┬────────────┘
               │              │
               ▼              ▼
         BD Actualiza    Volver a
         Recarga lista   estado inicial
         Imagen se       (campo vacío)
         asigna auto
               │
               ▼
         ┌──────────────┐
         │ Mostrar Lista│
         │ (actualizada)│
         └──────────────┘
```

---

## Conclusión de Arquitectura

✅ **Separación de responsabilidades:** 
   - Converter maneja la lógica de mapeo
   - ViewModel maneja la lógica de negocio
   - View maneja la presentación

✅ **Bajo acoplamiento:**
   - Converter no depende del ViewModel
   - Vista binds a través de interfaces estándar
   - Código reutilizable

✅ **Escalabilidad:**
   - Fácil agregar nuevos mapeos en converter
   - Patrón estándar de WPF
   - Sin cambios en estructura

✅ **Rendimiento:**
   - Conversion lazily (cuando se necesita)
   - Binding eficiente
   - Sin sobrecarga innecesaria
