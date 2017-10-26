SELECT v.Id, v.Name,m.Name,m.Age
FROM Vilians AS v
JOIN ViliansMinions AS vm ON v.Id=vm.VilianId
JOIN Minions AS m ON m.Id=vm.MinionId

--SELECT v.Name,count(vm.MinionId)as [Number of minions]
--FROM Vilians AS v
--JOIN ViliansMinions AS vm ON ViliansMinions.VilianId=Vilians.Id
--GROUP BY Vilians.Name
--HAVING count(*)>3
--ORDER BY [Number of minions] desc
