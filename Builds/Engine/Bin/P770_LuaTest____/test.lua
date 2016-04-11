-- import
package.path = "?.lua;" .. package.path
require("test2")
 
-- define name space
local m = {}
_G["test"] = m
 
-- Test Function
function m:test()
  	writeLine("うふふ～☆");
end