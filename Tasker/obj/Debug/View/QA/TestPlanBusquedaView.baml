   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      )    #Tasker.View.QA.TestPlanBusquedaView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0classesclr-namespace:Tasker.Classes     5        � DataContext  5
      �� 5       �� Path$ TestPlanBusquedaViewModel�6      Locator �� Source# [  6-   .��5      � -   5         �Name$ 
Contenedor��5      .+�5      � -   5        $ 	Principal��5   	    � RowDefinitions 5      �� 5      $
��auto �6   �� 5      $
��359* �6   .+�5      � -   5        $ 
Encabezado��5   
   $��5q�5      $��0��5       5      �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6    � ColumnDefinitions 5       �� 5!      $��82 �6   �� 5"      $	��258 �6   .+�5&      � -   5        $
 Menu��5&      $��0��6   $��1��6&   $��Right=�66   $��46��6R   .+�5'      ��  �� Orientation$ 
Horizontal=�6   $��1��6+   .+�5(      "   Tasker.Classes.ToolBarButton  $��Center=�6      ItemsTestPlanToolBar_confirm   MetroImageSource# [ 5)       � ToolTip$% Confirm The Selected Test Plan.��5*   0   ��5+      �� *ConfirmCommand+  5,      $��Center=�6     toolbar_cancel# [ 5-      $* $Return to the Main Test Plan Screen.��5.   0   ��5/      �� *CancelCommand+50      51      �� 55      $��
Date From:��6   $��1��6"   $��Center=�6/   �� 57      $��Date To:��6   $��2��6    $��Center=�6-   �� 59      $��Description:��6   $��3��6$   $��Center=�61   (  "System.Windows.Controls.DatePicker  5?      $��1��6   $��1��6!   $��5q�5@      $
��Left=�5B      $��Center=�5C      	   Text	 5A      �� *
DateFrom+
 �� Mode$
 TwoWay=�5C        5E      $��1��6   $��2��6!   $��5q�5F      $
��Left=�5H      $��Center=�5I      	 5G      �� *DateTo+$
 TwoWay=�5I      �� 5K      $��3��6   $��1��6*   $ 
Horizontal=�6:   .+�5L      �� $	��210��5M      $��5q�5O      ��5N      �� *Descripcion+$
 TwoWay=�5O        5Q      $��Center=�6(   $��5q�5R   (     toolbar_search# [ 5S      $( "Search based on selected criteria.��5T   (   ��5U      �� *SearchCommand+$
 TwoWay=�5V      5Z      � 5_      -   5        $ Items��5_   
   $��1��6    5a      �� 5b      $
��Auto �6   �� 5c      $	��30* �6   .+�5f      �� -   5         ��Name$ RevealContainer��5f      $��0��5g      $��0,0,0,0q�5h      ���	#3FD2D2D25k      �� 5l      $��1��6   ���	#4F4F4D4D
������   RedGlowItemContainer1#��[ 5o      ��5p      �� *TestPlanList+ �� SelectedItem 5q      �� *SelectedTestPlan+ J� InputBindings 5s      d� 5t       d� MouseAction$ LeftDoubleClicke�6    �� Command 5u      �� *NewSubTaskCommand+��5x      �� 5y      .R�5z   	   � ���	#20A9A9A9$
��Left=�5|      $��Center=�5}      $��5q�5~      $	��330��5      .+�5�   
   �  5�      �� 5�      $	��87* �6    5�      �� 5�      $	��15* �6   �� 5�      $	��25* �6   �� 5�      $	��15* �6   .+�5�      �� $��0��5�      $	��120��5�      $��13(�5�      $
��Auto��5�      $��5,0,0,0q�5�      $
��Bold"�5�        ItemText#���  5�      ��5�      �� *Descripcion+�� 5�      $��1��5�      $��11(�5�      $
��Auto��5�      $��5,0,0,0q�5�      #���  5�      ��5�      �� *Fecha+�� 5�      $
��Auto=�6   $��2��6>   .��5�      �� $��11(�5�   	   $
��Auto��5�   	   $��5,0,0,0q�5�   	   #���  5�   	   ��5�   	   �� *
Aplicacion+5�      5�      5�   
   5�   	   5�      5�      5�      5�      5�      