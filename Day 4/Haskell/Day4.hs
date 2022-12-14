import Data.List.Split

parseRange = map (\x -> read x::Int) . splitOn "-"
parseRanges = map parseRange . splitOn ","

containsFully [[x1, y1], [x2, y2]] = (x1 <= x2 && y1 >= y2) || (x2 <= x1 && y2 >= y1)
hasOverlap [[x1, y1], [x2, y2]] = x1 <= y2 && x2 <= y1

main = do
    input <- readFile "..\\input.txt"
    let rangePairs = map parseRanges $ lines input
    -- #1
    print $ length $ filter containsFully rangePairs
    -- #2
    print $ length $ filter hasOverlap rangePairs