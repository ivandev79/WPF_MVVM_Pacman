﻿#pragma checksum "..\..\..\Views\AdvancedGhostOptionWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D0F1AC3645235D1DA5BCC86DDAFCF3103755BCA1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Pacman.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Pacman.Views {
    
    
    /// <summary>
    /// AdvancedGhostOptionWindow
    /// </summary>
    public partial class AdvancedGhostOptionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 70 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel paramsPanel;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GreenCountTextBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GreenSpeedComboBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BlueCountTextBox;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox BlueSpeedComboBox;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RedCountTextBox;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RedSpeedComboBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Pacman;component/views/advancedghostoptionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            ((Pacman.Views.AdvancedGhostOptionWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 55 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 55 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.paramsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.GreenCountTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 75 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            this.GreenCountTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GreenSpeedComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.BlueCountTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 88 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            this.BlueCountTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BlueSpeedComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.RedCountTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 101 "..\..\..\Views\AdvancedGhostOptionWindow.xaml"
            this.RedCountTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.RedSpeedComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

