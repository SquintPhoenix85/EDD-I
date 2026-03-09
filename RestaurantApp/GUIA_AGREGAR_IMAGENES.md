# ⚙️ Guía Rápida - Agregar Nuevas Imágenes Específicas

## Si Necesitas Agregar Más Imágenes...

A veces necesitarás que ciertos alimentos tengan su propia imagen. Aquí te muestro cómo hacerlo en 2 pasos.

---

## Paso 1: Agregar la Imagen al Proyecto

### 1.1 Coloca la imagen PNG en la carpeta

```
RestaurantApp/
├── Resources/
│   ├── agua.png
│   ├── capuchino.png
│   ├── cruasan.png
│   ├── expresso.png
│   ├── tortadechocolate.png
│   ├── comida.png
│   └── NUEVA_pizza.png          ← Coloca aquí tu nueva imagen
```

### 1.2 Nombra el archivo claramente

- Sin espacios: `pizza.png` no `pizza con queso.png`
- Minúsculas: `pizza.png` no `PIZZA.png`
- Solo letras números y guiones: `pizza_jamon.png`

Ejemplos:
- ✅ `pizza.png`
- ✅ `hamburguesa.png`
- ✅ `cafe_espresso.png`
- ✅ `jugo_naranja.png`
- ❌ `Pizza.PNG`
- ❌ `Mi Pizza.png`
- ❌ `pizza con queso.png`

---

## Paso 2: Editar el Converter

### 2.1 Abre el archivo

Ubicación: `Utilities/PlatoImageConverter.cs`

Contenido actual:
```csharp
return nombreLower switch
{
    "agua" => "pack://application:,,,/Resources/agua.png",
    "capuchino" => "pack://application:,,,/Resources/capuchino.png",
    "cruasan" => "pack://application:,,,/Resources/cruasan.png",
    "espresso" or "expresso" => "pack://application:,,,/Resources/expresso.png",
    "torta de chocolate" or "tortadechocolate" or "torta chocolate" => "pack://application:,,,/Resources/tortadechocolate.png",
    _ => "pack://application:,,,/Resources/comida.png"
};
```

### 2.2 Agrega tu nueva línea

Busca esta sección y agrega ANTES del último `_`:

```csharp
return nombreLower switch
{
    "agua" => "pack://application:,,,/Resources/agua.png",
    "capuchino" => "pack://application:,,,/Resources/capuchino.png",
    "cruasan" => "pack://application:,,,/Resources/cruasan.png",
    "espresso" or "expresso" => "pack://application:,,,/Resources/expresso.png",
    "torta de chocolate" or "tortadechocolate" or "torta chocolate" => "pack://application:,,,/Resources/tortadechocolate.png",
    "pizza" => "pack://application:,,,/Resources/pizza.png",        // ← NUEVA LÍNEA
    "hamburguesa" => "pack://application:,,,/Resources/hamburguesa.png",  // ← NUEVA LÍNEA
    _ => "pack://application:,,,/Resources/comida.png"
};
```

### 2.3 Guarda y compila

```
Archivo → Guardar (Ctrl+S)
Compilar → Compilar solución (Ctrl+Shift+B)
```

✅ Si sale "Compilación correcta", ¡listo!

---

## 📋 Ejemplos Completos

### Ejemplo 1: Agregar Pizza

**Archivo:** `Resources/pizza.png` ✅
**Converter:**
```csharp
"pizza" => "pack://application:,,,/Resources/pizza.png",
```
**Resultado:** Al escribir "pizza", asigna pizza.png

---

### Ejemplo 2: Agregar Múltiples Variaciones

**Archivos:** 
- `Resources/cafe.png` ✅
- `Resources/cafe_espresso.png`

**Converter:**
```csharp
"cafe" or "café" or "coffee" => "pack://application:,,,/Resources/cafe.png",
"cafe espresso" or "cafe_espresso" => "pack://application:,,,/Resources/cafe_espresso.png",
```

**Resultado:** 
- Escribes "café" → cafe.png
- Escribes "café espresso" → cafe_espresso.png

---

### Ejemplo 3: Frutas y Jugos

**Archivos:**
- `Resources/jugo_naranja.png`
- `Resources/jugo_manzana.png`
- `Resources/jugo_fresa.png`

**Converter:**
```csharp
"jugo de naranja" or "jugo naranja" => "pack://application:,,,/Resources/jugo_naranja.png",
"jugo de manzana" or "jugo manzana" => "pack://application:,,,/Resources/jugo_manzana.png",
"jugo de fresa" or "jugo fresa" => "pack://application:,,,/Resources/jugo_fresa.png",
```

---

## 🎯 Patrones Útiles

### Patrón 1: Una palabra simple
```csharp
"pizza" => "pack://application:,,,/Resources/pizza.png",
```
Escribe: "pizza" ✅

---

### Patrón 2: Múltiples variaciones
```csharp
"cafe" or "café" or "coffee" => "pack://application:,,,/Resources/cafe.png",
```
Escribe: "cafe" ✅, "café" ✅, "coffee" ✅

---

### Patrón 3: Frase completa
```csharp
"jugo de naranja" or "jugo naranja" => "pack://application:,,,/Resources/jugo_naranja.png",
```
Escribe: "jugo de naranja" ✅, "jugo naranja" ✅

---

### Patrón 4: Con espacios intermedios
```csharp
"torta de chocolate" or "torta chocolate" or "tortadechocolate" => "...",
```
Escribe: "torta de chocolate" ✅, "torta chocolate" ✅, "tortadechocolate" ✅

---

## ❌ Errores Comunes

### Error 1: Olvidar la coma
```csharp
❌ "pizza" => "pack://application:,,,/Resources/pizza.png"  // Falta coma
   "hamburguesa" => ...

✅ "pizza" => "pack://application:,,,/Resources/pizza.png",  // Con coma
   "hamburguesa" => ...
```

### Error 2: Ruta incorrecta
```csharp
❌ "pizza" => "pack://application:,,,/Resources/pizza.PNG",  // Mayúscula .PNG
✅ "pizza" => "pack://application:,,,/Resources/pizza.png",  // Minúscula .png
```

### Error 3: Falta el último `_`
```csharp
❌ return nombreLower switch
   {
       "pizza" => ...,
   };  // No hay _ por defecto
   
✅ return nombreLower switch
   {
       "pizza" => ...,
       _ => "pack://application:,,,/Resources/comida.png"
   };  // _ al final para excepciones
```

### Error 4: Cambiar la imagen por defecto
```csharp
❌ _ => "pack://application:,,,/Resources/comida_generica.png",  // Archivo no existe

✅ _ => "pack://application:,,,/Resources/comida.png",  // Archivo que existe
```

---

## 🧪 Prueba tu nueva imagen

### Paso 1: Compila
Verifica que salga "Compilación correcta"

### Paso 2: Ejecuta
Abre la aplicación

### Paso 3: Prueba
1. Ve a Platos
2. Escribe el nombre exacto (ej: "pizza")
3. Observa el preview: debe mostrar tu imagen ✅
4. Escribe el precio
5. Registra
6. Verifica que en la tarjeta aparezca tu imagen

---

## 📁 Estructura Final con Nuevas Imágenes

```
RestaurantApp/
├── Resources/
│   ├── agua.png                    (ya existe)
│   ├── capuchino.png              (ya existe)
│   ├── cruasan.png                (ya existe)
│   ├── expresso.png               (ya existe)
│   ├── tortadechocolate.png       (ya existe)
│   ├── comida.png                 (ya existe)
│   ├── pizza.png                  (✅ NEW)
│   ├── hamburguesa.png            (✅ NEW)
│   ├── jugo_naranja.png           (✅ NEW)
│   └── jugo_manzana.png           (✅ NEW)
│
├── Utilities/
│   └── PlatoImageConverter.cs      (editado)
```

---

## 📞 Resumen Rápido

| Necesito | Pasos |
|---|---|
| Agregar pizza | 1. Coloca pizza.png en Resources/<br/>2. Agrega `"pizza" => "pack://application:,,,/Resources/pizza.png",` en converter<br/>3. Compila |
| Múltiples nombres | Usa `or`: `"pizza" or "pizzas" => ...` |
| Frase completa | Escribe exacto: `"jugo de naranja" => ...` |
| Variaciones | Agrega todas las opciones: `"café" or "cafe" or "coffee"` |

---

## 💡 Pro Tips

✨ **Tip 1:** Usa nombres simples y cortos
- Mejor: "pizza"
- Evita: "pizza_grande_italiana_especial"

✨ **Tip 2:** Ordena alfabéticamente el converter
- Hace más fácil encontrar cosas después
- Ejemplo:
  ```csharp
  "agua" => ...,
  "cafe" => ...,
  "capuchino" => ...,
  "cruasan" => ...,
  "espresso" => ...,
  "hamburguesa" => ...,
  "jugo" => ...,
  "pizza" => ...,
  ```

✨ **Tip 3:** Usa comentarios para secciones
```csharp
// Bebidas
"agua" => ...,
"cafe" => ...,

// Comidas
"hamburguesa" => ...,
"pizza" => ...,
```

✨ **Tip 4:** Prueba antes de usarlo
- Abre la app
- Escribe el nombre
- Verifica en preview
- Confirma la imagen

---

## 🆘 Si Algo Falla

**P: Compiló con error**
R: Verifica:
- ¿Olvidaste la coma después de la ruta?
- ¿La sintaxis de `or` es correcta?
- ¿El último `_` está presente?

**P: No aparece la imagen**
R: Verifica:
- ¿El nombre que escribes coincide exacto (mayúsculas)?
- ¿El archivo PNG existe en Resources/?
- ¿La ruta en converter es correcta?

**P: Muestra comida.png en lugar de mi imagen**
R: Significa que el nombre NO coincidió. Revisa:
- ¿Escribiste el nombre exacto?
- ¿Hay espacios extras?
- ¿Las mayúsculas coinciden? (usa `ToLower()` en converter)

---

¡Ahora puedes agregar todas las imágenes que necesites! 🎉
