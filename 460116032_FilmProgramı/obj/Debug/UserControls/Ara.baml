   M S B A M L   `   `   `  ����R  N460116032_FilmProgramı, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:_460116032_FilmProgramı.UserControls%_460116032_FilmProgramı.UserControls  5      /    )_460116032_FilmProgramı.UserControls.Ara   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:_460116032_FilmProgramı.UserControls     5        � 	Resources  5	         Load �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%�   &	        �� 5
   
   .��5      U� $ (FrameworkElement.Width)�6,   $ txtAra��6a   .\�5        $ 0��6'   $ 0Q�63     5      $ 0:0:0.1��6'   $	 280Q�69   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� #��[  6   5      .��5      � $
��2000��6   ��5   
   �� 5       �� Color$ Black��6    �� Opacity$
 0.90Q�6,   .+�5   
   �� .+�5      �� -   5        	 ��Name$	 	txtBaslik��5      $��	0,100,0,0q�6,   .+�5      � .+�5       �� -   5        
 ��Name$
 txtAra��5       $	��280��6.   
���   �$��10q�6M   $��2,0,0,0q�6Y   $��28��6l   $��18(�6x   .��6�   ��5!      �� 5"      $ White��6.   $	 0.5Q�6<   .��5#   .   5$      �� 5%       J� IsHitTestVisible .  $��28��69   $��18(�6E   $��Araki Bulasın!��6S   $��Center=�6i   $
��Left=�6�   $��15,5,0,0q�6�   
���������5&      �� 5'       �� 
TargetType" ��6$   .�5(   "   ��  �� Property � y� �� Value$ 	Collapsed=�6?    �� Triggers 5)   "   �� 5*   &    �� Binding 62   �� *Text+ �� ElementName$ txtAra�� �� Value  6_   .a�5+   *   ��  � y�$ Visible=�6G   5,   '   5.      50      51      52      �� 54      .+�55      �� -   5         ��Name$ cntrlAra��55      $��10q�61     ItemsPanelTemplate1#���  6=     DataTemplate1#���  6p     56       � Cursor
 � Tag��%=  &	       &	 (     �� 57      .R�58      �� 59      �� 5:      .R�5<      �� .+�5=   "   �� $��0,1,0,1q�6)   $��0,5,0,5q�6C   
�������._�5>   &   �� -   5        $
 Hand��5>   0   
�������$��16(�6p   ��6>   �� *
FilmAdı+ 6~   �� *FilmId+6�   5?   #   5@      5B      5C      5E      5F      �� 5G      -   5        $	 	spİcerik��5G      .+�5H       � -   5          �Name$	 frm��5H      6'   5I      5J      5K      5L      