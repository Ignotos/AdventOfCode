import Data.List (nub)

hasDuplicates xs = length xs /= length (nub xs)

getMarker str size pos
    | hasDuplicates (take size str) = getMarker (drop 1 str) size (pos + 1) 
    | otherwise                     = pos

main = do
    input <- readFile "..\\input.txt"
    -- #1
    print $ getMarker input 4 4
    -- #2
    print $ getMarker input 14 14