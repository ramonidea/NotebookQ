   M S B A M L   `   `   `  ���� D  @notebookQ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:notebookQ	notebookQ      notebookQ.Schedule   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     )localclr-namespace:notebookQ     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMaterialDesignThemes.Wpf, Version=1.4.1.485, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Schedule��$	��360��   MaterialDesignBody#���   #��� ��$��Medium"�$��14(�$X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)� � WindowStartupLocation$ CenterOwner=�$	��700�� � WindowStyle$ 
ToolWindow=�$��5q�.��� $��Stretch=�$��Stretch=� � RowDefinitions �� $
��665* �.+��� $��1��
���   �$��1q�$
��Left=�$	��664��$��1��$	��Top=�$	��342��$��0,3,0,-2q�._�� $��0,0,-1,0q� �� $
��157* ��� $��5 ��� $
��169* �.+��� $��1��$��Stretch=�#��� ��$��Stretch=�$
��Auto=��� -   $��slist��$��0��#��� ��$��0q�����  �� 
TargetType" �� �� BasedOn# [B��.���  �� Property � �� �� Value$ Stretch=����� .R��� $��listsp��-   $��Stretch=�#��� ��$	��300��$
��Auto=�
 J� Uid �� *UID+	 � ContextMenu	 �� .@�m� -   $��Delete��.+��� -   $�� ��
 d� 	IsChecked
 �� *	butfunc+�� $��14(�$��Stretch=����	#FF767575���� *Time+�� $��16(�$��Stretch=�$��Stretch=����� *	Content+�� $��2��.+�(  "System.Windows.Controls.DatePicker  -   $��Dates��)  #MaterialDesignThemes.Wpf.TimePicker  -   $��Times��#  MaterialDesignThemes.Wpf.Card  -   $
��card��$��Stretch=�$	��314��#��� ������ .+��� �� $��Content:��$��20(� �� TextAlignment$ Center=��� -   $��content�� �� AcceptsReturn . $��Stretch=�$
��Auto=�$��86���� $�� ��$��28���� $��Place:��$��20(�$ Center=��� -	   $��place��$��Stretch=�$
��Auto=�$��42���� $��24���� -
    ��Name$ update��$��Create��$��Stretch=�$��22(�