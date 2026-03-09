# Fonts Directory

## Poppins Font

This directory contains custom fonts used in the RestaurantApp project.

### Setup Instructions

1. **Download Poppins Font:**
   - Download Poppins from: https://fonts.google.com/specimen/Poppins
   - Extract the TTF files from the downloaded ZIP

2. **Add Font Files:**
   - Place the Poppins TTF files (Poppins-Regular.ttf, Poppins-SemiBold.ttf, Poppins-Bold.ttf, etc.) in this `Fonts` folder

3. **Update Project File:**
   - Ensure the .csproj file includes the font files as embedded resources:
   ```xml
   <ItemGroup>
     <Resource Include="Fonts/*.ttf" />
   </ItemGroup>
   ```

4. **Font Reference in XAML:**
   - In App.xaml, fonts are referenced as: `pack://application:,,,/Fonts/#Poppins`
   - This works for any `.ttf` file placed in this directory

### Font Files Included

This project uses:
- **Poppins-Regular.ttf** - Regular weight (400)
- **Poppins-SemiBold.ttf** - SemiBold weight (600)

### Font Resources in XAML

Available font resources defined in App.xaml:

1. **PoppinsRegular** - For body text and regular content
   ```xaml
   <TextBlock FontFamily="{StaticResource PoppinsRegular}" FontSize="13" Text="Body Text"/>
   ```

2. **PopinsSemiBold** - For headings and emphasized content
   ```xaml
   <TextBlock FontFamily="{StaticResource PopinsSemiBold}" FontSize="14" Text="Heading"/>
   ```

3. **PoppinsFont** - Default to Regular (for backward compatibility)
   ```xaml
   <TextBlock FontFamily="{StaticResource PoppinsFont}" FontSize="13" Text="Text"/>
   ```

### Usage in Styles

The following controls already use Poppins fonts:
- **Button** - PopinsSemiBold, 14px
- **TextBox** - PoppinsRegular, 13px
- **ComboBox** - PoppinsRegular, 13px
- **GroupBox** - PopinsSemiBold, 13px
- **DataGrid** - PoppinsRegular, 12px
- **DataGridColumnHeader** - PopinsSemiBold, 12px
- **TabItem** - PopinsSemiBold, 13px
