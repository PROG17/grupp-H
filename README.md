# grupp-H
START
MEDANS SUDOKUT INNEHÅLLER NOLLOR SÅ
	FÖR VARJE RAD I SUDOKUT
		FÖR VARJE RUTA I RADEN
			OM RUTA INNEHÅLLER NOLLA
				FÖR VARJE TAL MELLAN 1-9
					OM TAL INTE FINNS I BOX, RAD ELLER KOLUMN SÅ
						SPARA TAL I LISTA FÖR POTENTIELLA RÄTTA
						KOLLA NÄSTA TAL
					ANNARS SÅ
						KOLLA NÄSTA TAL	
				OM LISTAN FÖR POTENTIELLA TAL FÖR RUTA ÄR == 1 SÅ
					BYTT UT "0" MOT TALET
					KOLLA NÄSTA RUTA
				ANNARS SÅ 
					KOLLA NÄSTA RUTA
			ANNARS SÅ
				KOLLA NÄSTA RUTA
	OM SUDOKUT GÅTT ETT VARV UTAN ATT FÖRÄNDRATS
		SKRIV UT ATT SUDOKUT INTE GICK ATT LÖSA
		SLUT
SLUT MEDANS
	SKRIV UT SUDOKUPLANEN I KONSOLLEN
SLUT
