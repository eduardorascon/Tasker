   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.HelpersTasker.Helpers  5   	   )clr-namespace:Tasker.ViewTasker.View  5	   	   /clr-namespace:Tasker.ClassesTasker.Classes  5
   	   &     Tasker.View.SprintsWorkSpaceView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (ignorehttp://www.ignore.com     5   	   0helpersclr-namespace:Tasker.Helpers     5   	   G CPixelLab.Wpf, Version=6.0.2.0, Culture=neutral, PublicKeyToken=nullAwpf7http://schemas.thinkpixellab.com/2011/xaml/presentation  5   	   $viewclr-namespace:Tasker.View  5	   	   *classesclr-namespace:Tasker.Classes  5
   	     � DataContext  5   	   �� *SprintsWorkSpaceViewModel+   Locator �� Source# [   � 	Resources 5      ,   &Tasker.Helpers.InverseBooleanConverter  InverseBoolConverter��%   &	         5   
   6*   5      .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��25 �6   �� 5      $
��Auto �6   �� 5      $
��300* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��1��6.   .+�5      "   Tasker.Classes.ToolBarButton  $	��Top=�6$     toolbar_new   MetroImageSource# [ 5   $    � ToolTip$ Add a new Category��6T   {�5   $   �� *
IsEditMode+ �� 	Converter# [ ��5   $   �� *AddSprintCommand+  5       $	��Top=�6$     toolbar_edit# [ 5!   $   $ Edit Category��6U   {�5"   $   �� *
IsEditMode+# [ ��5#   $   �� *EditSprintCommand+  5$      $	��Top=�6$     toolbar_save# [ 5%   $   $ Save Category changes��6U   {�5&   $   �� *
IsEditMode+��5'   $   �� *SaveSprintCommand+  5)      $	��Top=�6$     toolbar_cancel# [ 5*   $   $ Cancel��6W   {�5+   $   �� *
IsEditMode+��5,   $   �� *CancelEditSprintCommand+  5-      $	��Top=�6$     toolbar_switch# [ 5/   $   $ Enable / Disable Category��6W   {�5.   $   �� *
IsEditMode+# [ ��50   $   �� *ActivateCategoryCommand+51      � 53   
   $��0��6   ��54      �� 55        AccentColor �� Color# �  6"   6J   .+�57   
   �� $	��3,2q�6   $��My Categories��6"   58      �� 5:   
   -   5        	 ��Name$	 RevealContainer��5:      $��2��5;      ���	#3FD2D2D2._�5=        PixelLab.Wpf.Reveal  -   5        
  Name$
 CategoryDetailsReveal��5=         Duration$	 150Q�5>         
IsExpanded 5?      �� *
IsEditMode+._�5@         Tasker.View.SprintView  -   5          Name$ OSprintView��5@   "   $	��160��67   5A      5B         Tasker.View.SprintsView  5D   
   $��3��6   6*   5F      5G      