   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.HelpersTasker.Helpers  5   	   )clr-namespace:Tasker.ViewTasker.View  5	   	   /clr-namespace:Tasker.ClassesTasker.Classes  5
   	   %    Tasker.View.IssuesWorkSpaceView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (ignorehttp://www.ignore.com     5   	   0helpersclr-namespace:Tasker.Helpers     5   	   G CPixelLab.Wpf, Version=6.0.2.0, Culture=neutral, PublicKeyToken=nullAwpf7http://schemas.thinkpixellab.com/2011/xaml/presentation  5   	   $viewclr-namespace:Tasker.View  5	   	   *classesclr-namespace:Tasker.Classes  5
   	     � DataContext  5   	   �� *IssuesWorkSpaceViewModel+   Locator �� Source# [   � 	Resources 5      ,   &Tasker.Helpers.InverseBooleanConverter  InverseBoolConverter��%   &	         5   
   6*   5      .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��25 �6   �� 5      $
��Auto �6   �� 5      $
��300* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��1��6.   .+�5      "   Tasker.Classes.ToolBarButton  $	��Top=�6$     appbar_refresh   MetroImageSource# [ 5   $    � ToolTip$ Refresh JIRA Issues��6W   {�5   $   �� *
IsEditMode+ �� 	Converter# [ ��5   $   �� *RefreshIssuesCommand+5!      � 5#   
   $��0��6   ��5$      �� 5%        AccentColor �� Color# �  6"   6J   .+�5'   
   �� $	��3,2q�6   $��My JIRA issues��6"   5(      �� 5*   
   -   5        	 ��Name$	 RevealContainer��5*      $��2��5+      ���	#3FD2D2D2._�5-        PixelLab.Wpf.Reveal  -   5        
  Name$
 CategoryDetailsReveal��5-         Duration$	 150Q�5.         
IsExpanded 5/      �� *
IsEditMode+._�50         Tasker.View.CategoryView  -   5          Name$ OCategoryView��50   $   $	��160��6;   51      52         Tasker.View.IssueListView  54   
   $��3��6   6,   56      57      