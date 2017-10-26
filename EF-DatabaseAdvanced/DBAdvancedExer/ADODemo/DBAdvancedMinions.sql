--SELECT Name,count(*)as [Number of minions]
--FROM Vilians
--JOIN ViliansMinions on ViliansMinions.VilianId=Vilians.Id
--GROUP BY Vilians.Name
--HAVING count(*)>3
--ORDER BY count(*) desc

SELECT v.Id, v.Name,m.Name,m.Age
FROM Vilians AS v
JOIN ViliansMinions AS vm ON v.Id=vm.VilianId
JOIN Minions AS m ON m.Id=vm.MinionId

