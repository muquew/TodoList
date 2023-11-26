﻿#pragma checksum "..\..\..\..\WindowItem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "897A14769504484DE5EF1377D59B5E45F949D027"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TodoList;


namespace TodoList {
    
    
    /// <summary>
    /// WindowItem
    /// </summary>
    public partial class WindowItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 59 "..\..\..\..\WindowItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit check;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\WindowItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image svg;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\WindowItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.DateEdit date;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\WindowItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock text;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TodoList;component/windowitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\WindowItem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 33 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SetFont);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 34 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SetColor);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 35 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ChangeText);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 36 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveSelf);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 38 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveFirst_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 39 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveUp_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 40 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveDown_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 41 "..\..\..\..\WindowItem.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveLast_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.check = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 10:
            this.svg = ((System.Windows.Controls.Image)(target));
            
            #line 64 "..\..\..\..\WindowItem.xaml"
            this.svg.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Svg_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.date = ((DevExpress.Xpf.Editors.DateEdit)(target));
            
            #line 77 "..\..\..\..\WindowItem.xaml"
            this.date.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.UpdateSelectedTime);
            
            #line default
            #line hidden
            return;
            case 12:
            this.text = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

