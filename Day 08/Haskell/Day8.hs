import Data.Char (digitToInt)

isEdge :: Int -> Int -> Int -> Int -> Bool
isEdge x y w h = x == 0 || y == 0 || x == w || y == h 

isVisibleFromLeft' :: [[Int]] -> Int -> Int -> Int -> Bool
isVisibleFromLeft' grid cursorPosition y treeValue 
    | cursorValue >= treeValue = False
    | cursorPosition == 0      = cursorValue < treeValue
    | cursorValue < treeValue  = isVisibleFromLeft' grid (cursorPosition - 1) y treeValue
    where cursorValue = ((grid !! y) !! cursorPosition)

isVisibleFromLeft :: [[Int]] -> Int -> Int -> Bool
isVisibleFromLeft grid x y = isVisibleFromLeft' grid (x - 1) y ((grid !! y) !! x)

isVisibleFromRight' :: [[Int]] -> Int -> Int -> Int -> Int -> Bool
isVisibleFromRight' grid cursorPosition y w treeValue 
    | cursorValue >= treeValue = False
    | cursorPosition == w      = cursorValue < treeValue
    | cursorValue < treeValue  = isVisibleFromRight' grid (cursorPosition + 1) y w treeValue
    where cursorValue = ((grid !! y) !! cursorPosition)

isVisibleFromRight :: [[Int]] -> Int -> Int -> Int -> Bool
isVisibleFromRight grid x y w = isVisibleFromRight' grid (x + 1) y w ((grid !! y) !! x)

isVisibleFromTop' :: [[Int]] -> Int -> Int -> Int -> Bool
isVisibleFromTop' grid cursorPosition x treeValue 
    | cursorValue >= treeValue = False
    | cursorPosition == 0      = cursorValue < treeValue
    | cursorValue < treeValue  = isVisibleFromTop' grid (cursorPosition - 1) x treeValue
    where cursorValue = ((grid !! cursorPosition) !! x)

isVisibleFromTop :: [[Int]] -> Int -> Int -> Bool
isVisibleFromTop grid x y = isVisibleFromTop' grid (y - 1) x ((grid !! y) !! x)

isVisibleFromBottom' :: [[Int]] -> Int -> Int -> Int -> Int -> Bool
isVisibleFromBottom' grid cursorPosition x h treeValue 
    | cursorValue >= treeValue = False
    | cursorPosition == h      = cursorValue < treeValue
    | cursorValue < treeValue  = isVisibleFromBottom' grid (cursorPosition + 1) x h treeValue
    where cursorValue = ((grid !! cursorPosition) !! x)

isVisibleFromBottom :: [[Int]] -> Int -> Int -> Int -> Bool
isVisibleFromBottom grid x y h = isVisibleFromBottom' grid (y + 1) x h ((grid !! y) !! x)

isVisible :: [[Int]] -> Int -> Int -> Int -> Int -> Int
isVisible grid x y w h
    | isEdge x y w h                 = 1
    | isVisibleFromLeft grid x y     = 1
    | isVisibleFromRight grid x y w  = 1
    | isVisibleFromTop grid x y      = 1
    | isVisibleFromBottom grid x y h = 1
    | otherwise                      = 0

getVisibleTreesCount' :: [[Int]] -> Int -> Int -> Int -> Int -> Int
getVisibleTreesCount' grid x y w h
    | x == w && y == h = isTreeVisible
    | x == w           = isTreeVisible + getVisibleTreesCount' grid 0 (y + 1) w h
    | otherwise        = isTreeVisible + getVisibleTreesCount' grid (x + 1) y w h
    where isTreeVisible = isVisible grid x y w h

getVisibleTreesCount :: [[Int]] -> Int
getVisibleTreesCount grid = getVisibleTreesCount' grid 0 0 ((length $ head grid) - 1) ((length grid) - 1)

getLeftScenicScore' :: [[Int]] -> Int -> Int -> Int -> Int
getLeftScenicScore' grid cursorPosition y treeValue 
    | cursorValue > treeValue = 0
    | cursorPosition == 0     = if cursorValue <= treeValue then 1 else 0
    | cursorValue < treeValue = 1 + getLeftScenicScore' grid (cursorPosition - 1) y treeValue
    | otherwise               = 1
    where cursorValue = ((grid !! y) !! cursorPosition)

getLeftScenicScore :: [[Int]] -> Int -> Int -> Int
getLeftScenicScore grid x y = if x == 0 then 0 else getLeftScenicScore' grid (x - 1) y ((grid !! y) !! x)

getRightScenicScore' :: [[Int]] -> Int -> Int -> Int -> Int -> Int
getRightScenicScore' grid cursorPosition y w treeValue 
    | cursorValue > treeValue = 0
    | cursorPosition == w     = if cursorValue <= treeValue then 1 else 0
    | cursorValue < treeValue = 1 + getRightScenicScore' grid (cursorPosition + 1) y w treeValue
    | otherwise               = 1
    where cursorValue = ((grid !! y) !! cursorPosition)

getRightScenicScore :: [[Int]] -> Int -> Int -> Int -> Int
getRightScenicScore grid x y w = if x == w then 0 else getRightScenicScore' grid (x + 1) y w ((grid !! y) !! x)

getTopScenicScore' :: [[Int]] -> Int -> Int -> Int -> Int
getTopScenicScore' grid cursorPosition x treeValue 
    | cursorValue > treeValue = 0
    | cursorPosition == 0     = if cursorValue <= treeValue then 1 else 0
    | cursorValue < treeValue = 1 + getTopScenicScore' grid (cursorPosition - 1) x treeValue
    | otherwise               = 1
    where cursorValue = ((grid !! cursorPosition) !! x)

getTopScenicScore :: [[Int]] -> Int -> Int -> Int
getTopScenicScore grid x y = if y == 0 then 0 else getTopScenicScore' grid (y - 1) x ((grid !! y) !! x)

getBottomScenicScore' :: [[Int]] -> Int -> Int -> Int -> Int -> Int
getBottomScenicScore' grid cursorPosition x h treeValue 
    | cursorValue > treeValue = 0
    | cursorPosition == h     = if cursorValue <= treeValue then 1 else 0
    | cursorValue < treeValue = 1 + getBottomScenicScore' grid (cursorPosition + 1) x h treeValue
    | otherwise               = 1
    where cursorValue = ((grid !! cursorPosition) !! x)

getBottomScenicScore :: [[Int]] -> Int -> Int -> Int -> Int
getBottomScenicScore grid x y h = if y == h then 0 else getBottomScenicScore' grid (y + 1) x h ((grid !! y) !! x)

getScenicScores' :: [[Int]] -> Int -> Int -> Int -> Int -> [Int]
getScenicScores' grid x y w h
    | x == w && y == h = [scenicScore]
    | x == w           = [scenicScore] ++ getScenicScores' grid 0 (y + 1) w h
    | otherwise        = [scenicScore] ++ getScenicScores' grid (x + 1) y w h
    where leftScenicScore   = getLeftScenicScore grid x y 
          rightScenicScore  = getRightScenicScore grid x y w
          topScenicScore    = getTopScenicScore grid x y
          bottomScenicScore = getBottomScenicScore grid x y h
          scenicScore       = leftScenicScore * rightScenicScore * topScenicScore * bottomScenicScore 

getScenicScores :: [[Int]] -> [Int]
getScenicScores grid = getScenicScores' grid 0 0 ((length $ head grid) - 1) ((length grid) - 1)

main = do
    input <- readFile "..\\input.txt"
    let grid = map (map digitToInt) $ lines input
    -- #1
    print $ getVisibleTreesCount grid 
    -- #2
    print $ maximum $ getScenicScores grid
