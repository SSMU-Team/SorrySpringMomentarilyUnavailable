import cv2
import numpy as np
     
FILES = ["statue_Pierre_LP_MetallicR_SmouthnessG.png", "statue_LP_MetalicR_SmoothnessG.png"]

for f in FILES :
    img = cv2.imread(f)
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    (R, G, B) = cv2.split(img)
    A = np.ones(R.shape, dtype=R.dtype) * 0
    #cv2.imshow("R" , R)
    #cv2.imshow("G" , G)
    #cv2.imshow("B" , B)
    #cv2.imshow("A" , A)
    output = cv2.merge([G, A, R, B])
    output = cv2.cvtColor(output, cv2.COLOR_RGBA2BGRA)
    #cv2.imshow("RGBA" , output)
    cv2.imwrite("Out_"+f, output)
