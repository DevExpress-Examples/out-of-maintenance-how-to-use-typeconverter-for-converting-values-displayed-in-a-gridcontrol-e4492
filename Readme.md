<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128632375/12.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4492)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MyObject.cs](./CS/GridControlTypeConverter/CustomData/MyObject.cs) (VB: [MyObject.vb](./VB/GridControlTypeConverter/CustomData/MyObject.vb))
* [MyPointConverter.cs](./CS/GridControlTypeConverter/CustomData/MyPointConverter.cs) (VB: [MyPointConverter.vb](./VB/GridControlTypeConverter/CustomData/MyPointConverter.vb))
* [TypeConverterHelper.cs](./CS/GridControlTypeConverter/CustomData/TypeConverterHelper.cs) (VB: [TypeConverterHelper.vb](./VB/GridControlTypeConverter/CustomData/TypeConverterHelper.vb))
* [Form1.cs](./CS/GridControlTypeConverter/Form1.cs) (VB: [Form1.vb](./VB/GridControlTypeConverter/Form1.vb))
* [LocalizedEnumConverter.cs](./CS/GridControlTypeConverter/LocalizedEnumConverter/LocalizedEnumConverter.cs) (VB: [LocalizedEnumConverter.vb](./VB/GridControlTypeConverter/LocalizedEnumConverter/LocalizedEnumConverter.vb))
* [ResourceEnumConverter.cs](./CS/GridControlTypeConverter/LocalizedEnumConverter/ResourceEnumConverter.cs) (VB: [ResourceEnumConverter.vb](./VB/GridControlTypeConverter/LocalizedEnumConverter/ResourceEnumConverter.vb))
* [Program.cs](./CS/GridControlTypeConverter/Program.cs) (VB: [Program.vb](./VB/GridControlTypeConverter/Program.vb))
<!-- default file list end -->
# How to use TypeConverter for converting values displayed in a GridControl


<p>This example demonstrates how to use the TypeConverter for values displayed in the GridControl. To accomplish this task, create an <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument1477"><u>unbound column</u></a> and handle the <a href="http://documentation.devexpress.com/#windowsforms/DevExpressXtraGridViewsBaseColumnView_CustomUnboundColumnDatatopic"><u>CustomUnboundColumnData</u></a> event.  For this, the TypeConverterHelper class has been created.</p><p>The first Grid demonstrates how to display localized enums (in the example the Russian, English, German, and French languages are available) and the second one contains a column where data can be displayed and edited in a custom manner.</p>

<br/>


