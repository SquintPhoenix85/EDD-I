# 🖼️ Sistema Automático de Asignación de Imágenes - Platos

## ¿Cómo Funciona?

Cuando registras un nuevo plato, **la imagen se asigna automáticamente** basándose en el nombre que escribiste, sin necesidad de seleccionar manualmente.

### Proceso Automático

```
1. Escribes el nombre del plato (ej: "capuchino")
        ↓
2. La imagen se muestra en preview en tiempo real
        ↓
3. Haces clic en "Registrar"
        ↓
4. El plato se guarda en la base de datos
        ↓
5. En la lista de platos (lado izquierdo), aparece la tarjeta con la imagen correcta
```

## 📋 Reglas de Asignación

### Imágenes Específicas por Nombre

| Nombre Escrito | Imagen Asignada | Archivo |
|---|---|---|
| `agua` | 💧 Botella de agua | `agua.png` |
| `capuchino` | ☕ Capuchino | `capuchino.png` |
| `cruasan` | 🥐 Croissant | `cruasan.png` |
| `espresso` o `expresso` | ☕ Espresso | `expresso.png` |
| `torta de chocolate`, `tortadechocolate`, `torta chocolate` | 🍰 Torta | `tortadechocolate.png` |
| **Cualquier otro nombre** | 🍽️ Comida genérica | `comida.png` |

### Ejemplos

✅ **Correcto:**
- Escribes: `capuchino` → Asigna: capuchino.png
- Escribes: `Agua` → Asigna: agua.png (no importa mayúsculas)
- Escribes: `CRUASAN` → Asigna: cruasan.png (no importa mayúsculas)
- Escribes: `espresso` → Asigna: expresso.png
- Escribes: `torta de chocolate` → Asigna: tortadechocolate.png
- Escribes: `cualquier comida` → Asigna: comida.png (por defecto)

❌ **Excepciones (usan comida.png):**
- Escribes: `café` → comida.png
- Escribes: `pizza` → comida.png
- Escribes: `arroz con pollo` → comida.png
- Escribes: `ensalada` → comida.png
- Escribes: `jugo de naranja` → comida.png

## 🔄 Cómo se Asigna

### Converter: PlatoImageConverter

El sistema usa un **ValueConverter** que funciona así:

```csharp
// Archivo: Utilities/PlatoImageConverter.cs

public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
{
    if (value is not string nombrePlato)
        return "pack://application:,,,/Resources/comida.png";

    var nombreLower = nombrePlato.ToLower().Trim();  // Convierte a minúsculas

    return nombreLower switch
    {
        "agua" => "pack://application:,,,/Resources/agua.png",
        "capuchino" => "pack://application:,,,/Resources/capuchino.png",
        "cruasan" => "pack://application:,,,/Resources/cruasan.png",
        "espresso" or "expresso" => "pack://application:,,,/Resources/expresso.png",
        "torta de chocolate" or "tortadechocolate" or "torta chocolate" => "pack://application:,,,/Resources/tortadechocolate.png",
        _ => "pack://application:,,,/Resources/comida.png"  // Por defecto
    };
}
```

## 📍 Dónde Sucede

### 1. **En el Formulario (Panel Derecho)**
```xaml
<!-- Vista previa mientras escribes -->
<Image Source="{Binding Nombre, Converter={StaticResource PlatoImageConverter}}"
       Stretch="UniformToFill"/>
```

### 2. **En las Tarjetas de Platos (Lista Izquierda)**
```xaml
<!-- Se asigna automáticamente al mostrar cada plato -->
<Image Source="{Binding Nombre, Converter={StaticResource PlatoImageConverter}}"
       Stretch="UniformToFill"/>
```

## 🎯 Flujo Práctico

### Ejemplo 1: Crear un Capuchino

1. **Escribes en el campo "Nombre":** `capuchino`
   - 🖼️ En "Vista Previa" aparece: Imagen de capuchino

2. **Escribes el precio:** `12000` pesos

3. **Haces clic en "Registrar"**
   - ✅ Se guarda en la BD

4. **Resultado en lista:**
   - Tarjeta con: Imagen capuchino + Nombre "capuchino" + Precio "$12000.00"

### Ejemplo 2: Crear una Hamburguesa

1. **Escribes en el campo "Nombre":** `hamburguesa`
   - 🖼️ En "Vista Previa" aparece: Imagen genérica (comida.png)

2. **Escribes el precio:** `8000` pesos

3. **Haces clic en "Registrar"**
   - ✅ Se guarda en la BD

4. **Resultado en lista:**
   - Tarjeta con: Imagen comida genérica + Nombre "hamburguesa" + Precio "$8000.00"

## 🎨 Ventajas del Sistema

✨ **Automático** - No necesitas seleccionar imagen manualmente
✨ **Inteligente** - Reconoce variaciones (espresso/expresso, mayúsculas/minúsculas)
✨ **Flexible** - Para alimentos desconocidos usa comida.png
✨ **Real-time** - Ves la imagen mientras escribes
✨ **Consistente** - Mismo nombre = misma imagen siempre

## 📝 Notas Importantes

- **Mayúsculas no importan:** `CAPUCHINO`, `capuchino`, `Capuchino` → todos muestran capuchino.png
- **Espacios se ignoran:** Espacios al inicio/final se eliminan automáticamente
- **Variaciones aceptadas:** 
  - `espresso` y `expresso` → misma imagen
  - `torta de chocolate`, `tortadechocolate`, `torta chocolate` → misma imagen
- **Si no encuentra coincidencia:** Usa siempre `comida.png`

## 🔧 Para Agregar Nuevas Imágenes

Si quieres agregar una imagen específica para otro tipo de comida:

1. Coloca la imagen PNG en `Resources/`
2. Abre `Utilities/PlatoImageConverter.cs`
3. Agrega una línea en el `switch`:
   ```csharp
   "tu_comida" => "pack://application:,,,/Resources/tu_comida.png",
   ```
4. Recompila el proyecto

**Ejemplo:**
```csharp
return nombreLower switch
{
    "agua" => "pack://application:,,,/Resources/agua.png",
    "capuchino" => "pack://application:,,,/Resources/capuchino.png",
    "pizza" => "pack://application:,,,/Resources/pizza.png",  // ← Nueva línea
    "cruasan" => "pack://application:,,,/Resources/cruasan.png",
    // ... resto de casos
    _ => "pack://application:,,,/Resources/comida.png"
};
```

## ✅ Verificación

Para verificar que todo funciona:

1. Escribe un nombre en el formulario
2. Observa la sección "Vista Previa"
3. La imagen debe cambiar según lo que escribas
4. Registra el plato
5. Aparecerá en la lista con la imagen correcta
