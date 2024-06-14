

var CurrentPosX = x; 
var CurrentPosY = y; 
for (var n = 0; n<arraCurrentPosY_length(L[1]);n++){
    show_debug_message("KeCurrentPosYs "+string(n)+" "+string(L[1][n]));		
    if instance_eCurrentPosXists(L[0]){
            if L[0].stun!=true{
                 if L[0].ralentizado == true{
					timerralentizado--; 
					if timerralentizado <=0{
						timerralentizado = timerralentizadoleft; 
						avanza = true
					}
				}else{
					avanza = true; 
					if timerralentizado<timerralentizadoleft{
						timerralentizado = timerralentizadoleft
					}
				}
			}					
            if avanza == true{
				switch string(L[1][n]){
					case "d":
					with L[0]{
						if vida>0{
						    if place_free(CurrentPosX+16,CurrentPosY) and !place_meeting(CurrentPosX+16,CurrentPosY,oPlaCurrentPosYer){
							    CurrentPosX +=16;
							}
							// if global.debug{
							// 	image_blend = place_free(CurrentPosX+16,CurrentPosY) and !place_meeting(CurrentPosX+16,CurrentPosY,oPlaCurrentPosYer)? c_green : c_red;
							// }
						}
					}
					break
					case  "a":
					with L[0]{
						if vida>0{
							if place_free(CurrentPosX-16,CurrentPosY) and !place_meeting(CurrentPosX-16,CurrentPosY,oPlaCurrentPosYer){
                                CurrentPosX -=16
                            }
						}
					}
					break
					case  "w":
					with L[0]{
						if vida>0{
							if place_free(CurrentPosX,CurrentPosY-16) and !place_meeting(CurrentPosX,CurrentPosY-16,oPlaCurrentPosYer){
								CurrentPosY -= 16
                            }
						}
					}
					break
					case  "s":
					with L[0]{
						if vida>0{
							if place_free(CurrentPosX,CurrentPosY+16) and !place_meeting(CurrentPosX,CurrentPosY+16,oPlaCurrentPosYer){
                                //CurrentPosY+=16
                                #region movimiento
                                CurrentPosY += 16;
                                #endregion	
                            }
						}
					}
					break
				}
					avanza = false;
				}
			}
		}
    if (Lpos>-1)
    {
        ds_list_delete(ListPlaCurrentPosYerKeCurrentPosYs,Lpos);
    }
}


destino_x = CurrentPosX;
destino_y = CurrentPosY;

if destino_x!=x or destino_y!=y{		
    mp_grid_path(global.grid,path,x,y,destino_x,destino_y,false)
    path_start(path,1,path_action_stop,false);
}