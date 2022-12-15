import Data.Char (isLower, isUpper, ord)
import Data.List.Split (chunksOf)

getPriority x
    | isUpper x = ord x - 38
    | isLower x = ord x - 96

getCommonItem ((x:xs), ys)
    | elem x ys = x
    | otherwise = getCommonItem (xs, ys)

getBadge [(x:xs), ys, zs]
    | elem x ys && elem x zs = x 
    | otherwise              = getBadge [xs, ys, zs]

splitCompartment x = splitAt (div (length x) 2) x

getPrioritySum = sum . map (getPriority . getCommonItem . splitCompartment)
getBadgePrioritySum = sum . map (getPriority . getBadge) . chunksOf 3 

main = do 
    input <- readFile "..\\input.txt"
    let rutsacks = lines input
    -- #1
    print $ getPrioritySum rutsacks
    -- #2
    print $ getBadgePrioritySum rutsacks