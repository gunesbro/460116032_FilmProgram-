   M S B A M L   `   `   `  ����R  N460116032_FilmProgramı, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:_460116032_FilmProgramı.UserControls%_460116032_FilmProgramı.UserControls  5   	   7 clr-namespace:_460116032.Windows_460116032.Windows  5   	   !    _460116032.Windows.HomePage   5   	   DUserControls3clr-namespace:_460116032_FilmProgramı.UserControls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:_460116032.Windows     5   	   -   5          � Title$  HomePage��5   	   $
��auto��6   $
��auto��6(    � 
ResizeMode$ CanMinimize=�65    � WindowState$ 	Maximized=�6N    � WindowStyle$ ThreeDBorderWindow=�6f   $	��760��6�   $
��1200��6�    � 	Resources 5	         İsikAcilma g� AutoReverse �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime	 O� Value8  2System.Windows.Media.Animation.EasingColorKeyFrame
 �� KeyTime �� Value��%t  &	        �� 5
   
    .  .��5      U� $O I(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Offset)�6,   $ 	stckİsik��6�   .\�5        $ 0:0:2��6'   $	 1Q�67   5      U� 5      $O I(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Offset)�6,   $ 	stckİsik��6�   .\�5        $ 0:0:2��6'   $	 0Q�67   5      �� 5      $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6+   $ 	stckİsik��6�   .g�5        $
 0:0:0.5��6&   $ 	#FF1C1C1F��68     5      $
 0:0:2��6&   $ White��66   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� #��[  6   5      .��5      � ���	#FF121314.+�5   
   ]� .+�5      �� $��	0,0,0.1,0q�6   
���   �._�5      ]� ���	#FF1C1C1F$	��100��63   .+�5       �� .+�5!      �� -   5         ��Name$ 	stckİsik��5!   %   $��10,20,10,10q�67   ��5"      
� 5#   "   .��5$   &   �  � Color$ 	#FF1C1C1F��63    � Offset$ 1Q�6E   � 5%   &   $ White��63   5&   #   .+�5(      �� -   5         ��Name$ imgCinsiyet��5(   $   $��80��69   $��80��6E   5)      5*      �� 5+      .+�5,      �� -   5        $��txtkAd��5,   (   
������� �� TextAlignment$ Center=�6N   6g   5-      �� 5.      .+�5/      �� $��/Images/cizgi.png��6$   $��15��6?   $��15��6K   6X   50      �� 51      $��1q�6!   
���   �$��10q�6I   6W   �� 53      -   5        $��10,50,10,10q�53   !   $��5,0,0,0q�66   $��50��6P   
��� � �
 � Tag$ brdrAra��6p   ._�54      �� -   5        $��Bottom=�54   )    � Cursor$
 Hand��6D   $	 Ara��6R   .+�55   "   �� $��20��6(   $��20��64   $��/Images/ara.png��6?   6Z   �� 56   "   $	��Ara��6,   $ Center=�67   $��18(�6N   
�������6v   57      58      �� 59      -   5        $��10,25,10,10q�59   !   $��5,0,0,0q�66   $��50��6P   ���	#FFF5FF08$ brdrAna��6t   ._�5:      �� -   5        $��Bottom=�5:   )   $
 Hand��6D   $ Anasayfa��6�   .+�5;   "   �� $��20��6(   $��20��64   $��/Images/home.png��6?   6[   �� 5<   "   $��Anasayfa��6,   $ Center=�6<   $��18(�6S   
�������6{   5=      5>      �� 5?      -	   5        $��10,25,10,10q�5?   !   $��5,0,0,0q�66   $��50��6P   
���  ��$ 	brdrListe��6q   ._�5@      �� -
   5        $��Bottom=�5@   )   $
 Hand��6D   $ Listem��6R   .+�5A   "   �� $��20��6(   $��20��64   $��/Images/liste.png��6?   6\   �� 5B   "   $��Listem��6,   $ Center=�6:   $��18(�6Q   
�������6y   5C      5D      �� 5E      -   5        $��10,25,10,10q�5E   !   $��5,0,0,0q�66   $��50��6P   ���	#FF004B88$ 	brdrTop20��6t   ._�5F      �� -   5        $��Bottom=�5F   )   $
 Hand��6D   $ Top20��6R   .+�5G   "   �� $��20��6(   $��20��64   $��/Images/top-movies.png��6?   6a   �� 5H   "   $��Top 10��6,   $ Center=�6:   $��18(�6Q   
�������6y   5I      5J      �� 5K      -   5        $��10,25,10,10q�5K   !   $��5,0,0,0q�66   $��50��6P   ���	#FF3D3038$ 
brdrVizyon��6t   ._�5L      �� -   5        $��Bottom=�5L   )   $
 Hand��6D   $
 Ekle��6R   .+�5M   "   �� $��20��6(   $��20��64   $��/Images/özel.png��6?   6[   �� 5N   "   $��Özel��6,   $ Center=�68   $��18(�6O   
�������6w   5O      5P      �� 5Q      -   5        $��10,25,10,10q�5Q   !   $��5,0,0,0q�66   $��50��6P   ���	#FF340088$ brdrAyarlar��6t   ._�5R      �� -   5        $��Bottom=�5R   )   $
 Hand��6D   $ Ayarlar��6�   .+�5S   "   �� $��20��6(   $��20��64   $��/Images/ayarlar.png��6?   6^   �� 5T   "   $��Ayarlar��6,   $ Center=�6;   $��18(�6R   
�������6z   5U      5V      5W      5X      5Y      �� 5Z      -   5         ��Name$
 cnvs��5Z      .+�5[      �� -   5        $
 stck��5[      5]      5^      �� 5_      .+�5`      �� .+�5a       � -   5          �Name$ 
frmGezinti��5a        � NavigationUIVisibility$ Hidden=�60   6S   5b      5c      5e      5h      5i      