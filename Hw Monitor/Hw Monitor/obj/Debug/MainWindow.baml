   M S B A M L   `   `   `  ����E  AHw Monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Hw_Monitor
Hw_Monitor  5   	   S OZedGraph, Version=5.1.5.28844, Culture=neutral, PublicKeyToken=02a83cbd123fcd605(clr-namespace:ZedGraph;assembly=ZedGraphZedGraph 5   	       Hw_Monitor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Hw_Monitor     5   	   8zed(clr-namespace:ZedGraph;assembly=ZedGraph     5   	     � Title$  
MainWindow��5
      $	��600��6   $	��800��6%   ���	#FF3A3333.��5      � $
��auto��6   $
��auto��6   .+�5   
   � -   5         �Name$ GridInformation��5      $
��auto��6(   $
��auto��65   $y�Hidden=�6C   
�������.+�5      �� -   5         ��Name$# textBox_networkAllInformation��5      
�������
���   �$��0q�6c   $y�Visible=�6w   $
��Left=�6�   $
��auto��6�   $��20,50,20,20q�6�    �� TextWrapping$
 Wrap=�6�   $��TextBox��6�   $	��Top=�6�   $
��auto��6  5      �� 5   
   $��50��6   $	��Top=�6!   $
��Left=�69    �� Orientation$ 
Horizontal=�6T   .+�5      o� -   5         o�Name$ menuView��5      $	��Top=�6%   .@�5      m� -   5        $��Ansicht��5      
 � Tag$ ansicht��6,   6:   5      o� 5      -   5        $
 menu��5      $	��Top=�6!   .@�5      m� -   5        $��Information��5      $ information��60   6B   5      5      � 5   
   -   5        $ Grid_Button��5      $
��auto��6$   $
��auto��61   $��	0,80,0,20q�6?   $
��Left=�6R   $	��Top=�6m    � ColumnDefinitions 5      �� 5       � RowDefinitions 5       �� 5!      $	��100 �6    �� 5"      $	��100 �6    �� 5#      $	��100 �6    �� 5$      $	��100 �6    .+�5)      �� -   5        	 ��Name$	 
Button_cpu��5)      $��1q�6)   $��0��6=   $��0��6M   $
��Left=�6Z   
����  �
�������$��0,20,0,0q�6�   $��Bottom=�6�   $	 Cpu��6�   $	��CPU��6�   $	��150��6�   $��80��6�   
 � Cursor$

 Hand��6   � ToolTip$ Prozessorinformationen��6!   J� Effect 5*      3  -System.Windows.Media.Effects.DropShadowEffect  5+         Color$ 	#FFF9ECEC��6'   5-      �� 5.      -	   5        $	 
button_ram��5.      $��1q�6)   $��Bottom=�6=   $��0��6_   $��1��6o   
���  ��
�������$��0,20,0,0q�6�   $	 Ram��6�   $��Arbeitsspeicher��6�   $
��Left=�6�   $	��150��6�   $��80��6  $

 Hand��6"  $" Arbeitsspeicherinformationen��60   5/        50      $ 	#FFF9ECEC��6'   52      �� 53      -
   5        $	 button_disk��53      $��1q�6*   $��0��6>   $��2��6N   $��Bottom=�6[   
��� � �
�������$��0,20,0,0q�6�   $
 Disk��6�   $��Datenträger��6�   $
��Left=�6�   $	��150��6�   $��80��6�   $

 Hand��6  $ Datenträgerinformationen��6,   54        55      $ 	#FFF9ECEC��6'   57      �� 58      -   5        $	 button_network��58      $��1q�6-   $��0��6A   $��3��6Q   $��Bottom=�6^   
��� ���
�������$��0,20,0,0q�6�   $ Network��6�   $��Netzwerk��6�   $
��Left=�6�   $	��150��6�   $��80��6  $

 Hand��6&  $ Netzwerkinformationen��65   59        5:      $ 	#FFF9ECEC��6'   5<      7  1System.Windows.Forms.Integration.WindowsFormsHost  5>      -   5          Name$ Zed_cpuMini��5>   W   $
��Left=�6   $��0��6:   $��0��6J   $��60��6l   $	��100��6x      Child. 5?        ZedGraph.ZedGraphControl  -   5        zedgraph_cpuMini��[ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"  System.Windows.Forms.Control   Width$ 30��5?   @      Height$ 30��6K   5@        5B      -   5        $ Zed_ramMini��5B      $
��Left=�64   $��0��6O   $��1��6_   $��60��6l   $	��100��6x   . 5C        -   5        zedgraph_ramMini��$ 30��5C   @   $ 30��6K   5D        5F      -   5        $ Zed_diskMini��5F      $
��Left=�65   $��0��6P   $��2��6`   $��60��6m   $	��100��6y   $

 Hand��6�   . 5G        -   5        zedgraph_diskMini��$ 30��5G   A   $ 30��6L   5H        5J      -   5        $ Zed_networkMini��5J      $
��Left=�68   $��0��6S   $��3��6c   $��60��6p   $	��100��6|   . 5K        -   5        zedgraph_networkMini��$ 30��5K   D   $ 30��6O   5L      5N      �� 5O   
   -   5        $ textBox_cpuInfo��5O      
�������
���   �$��0q�6Q   $y�Visible=�6e   $��Right=�6z   $	��220��6�   $��	0,0,20,10q�6�   $
 Wrap=�6�   $��TextBox��6�   $��Bottom=�6�   $	��500��6�   �� 5P   
   -   5        $ textBox_ramInfo��5P      
�������
���   �$��0q�6Q   $y�Hidden=�6e   $��Right=�6y   $	��200��6�   $��	0,0,20,20q�6�   $
 Wrap=�6�   $��TextBox��6�   $��Bottom=�6�   $	��500��6�   �� 5Q   
   -   5        $ textBox_diskInfo��5Q      
�������
���   �$��0q�6R   $y�Hidden=�6f   $��Right=�6z   $	��200��6�   $��	0,0,20,20q�6�   $
 Wrap=�6�   $��TextBox��6�   $��Bottom=�6�   $	��500��6�   �� 5R   
   -   5        $ textBox_networkInfo��5R      
�������
���   �$��0q�6U   $y�Hidden=�6i   $��Right=�6}   $	��200��6�   $��	0,0,20,20q�6�   $
 Wrap=�6�   $��TextBox��6�   $��Bottom=�6�   $	��500��6�     5T   
   -   5        $ Zed_cpu��5T      $y�Visible=�6,   $��Right=�6A   $	��300��6]   $��	0,20,20,0q�6j   $	��Top=�6}   $	��500��6�   . 5V        -   5        zedgraph_cpu��$	 500��5V   8   $	 200��6D   5X        5Z   
   -   5        $ Zed_ram��5Z      $y�Hidden=�6,   $��Right=�6@   $	��300��6\   $��	0,20,20,0q�6i   $	��Top=�6|   $	��500��6�   . 5\        -   5        zedgraph_ram��$	 500��5\   8   $	 200��6D   5]        5_   
   -   5        $ Zed_disk��5_      $y�Hidden=�6-   $��Right=�6A   $	��300��6]   $��	0,20,20,0q�6j   $	��Top=�6}   $	��500��6�   . 5`        -   5        zedgraph_disk��$	 500��5`   9   $	 200��6E   5a        5c   
   -   5        $ Zed_network��5c      $y�Hidden=�60   $��Right=�6D   $	��300��6`   $��	0,20,20,0q�6m   $	��Top=�6�   $	��500��6�   . 5d        -   5        zedgraph_network��$	 500��5d   <   $	 200��6H   5e      5f      5g      