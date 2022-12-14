import Data.List (nub)
import Data.List.Split (splitOn)

parseMove move =
    let parts = splitOn " " move
    in (head parts, (\x -> read x::Int) (last parts))

getDirection direction
    | direction >=  1 =  1
    | direction <= -1 = -1 
    | otherwise       =  0

moveTail (hx, hy) (tx, ty)
    | tx == hx && ty == hy                     = (tx, ty) -- Head and Tail are at the same position, no move
    | tx == hx && abs (ty - hy) == 1           = (tx, ty) -- Tail is above or below Head, no move
    | ty == hy && abs (tx - hx) == 1           = (tx, ty) -- Tail is to the right or left of Head, no move
    | abs (tx - hx) == 1 && abs (ty - hy) == 1 = (tx, ty) -- Tail is diagonal to Head, no move
    | otherwise                                = (tx + getDirection (hx - tx), ty + getDirection (hy - ty))

moveHead (x, y) "R" = (x + 1, y)
moveHead (x, y) "L" = (x - 1, y)
moveHead (x, y) "U" = (x, y + 1)
moveHead (x, y) "D" = (x, y - 1)

moveTails _ [] = [] 
moveTails headPos (tailPos:positions) = 
    let newTailPos = moveTail headPos tailPos
    in  newTailPos : moveTails newTailPos positions

recordMove _ (_, 0) = []
recordMove positions (move, steps) =
    let newHeadPos   = moveHead (head positions) move
        newPositions = newHeadPos : moveTails newHeadPos (tail positions)        
    in  newPositions : recordMove newPositions (move, steps - 1)

recordMoves _ [] = []
recordMoves positions (move:moves) =
    let newPositions = recordMove positions move
    in newPositions ++ recordMoves (last newPositions) moves

main = do
    input <- readFile "..\\input.txt"
    let moves = map parseMove $ lines input
    -- #1
    print $ length $ nub $ map last $ recordMoves [(0, 0), (0, 0)] moves
    -- #2
    print $ length $ nub $ map last $ recordMoves [(0, 0) | _ <- [0..9]] moves