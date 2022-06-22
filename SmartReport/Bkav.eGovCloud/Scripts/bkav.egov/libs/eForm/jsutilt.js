// Removes first whitespaces
function LTrim(value) {
    var re = /\s*((\S+\s*)*)/;
    return value.replace(re, "$1");
}

// Removes ending whitespaces
function RTrim(value) {
    var re = /((\s*\S+)*)\s*/;
    return value.replace(re, "$1");
}

// Removes leading and ending whitespaces
function trim(value) {
    return LTrim(RTrim(value));
}

// Remove the second item from the array:                     array.remove(1);
// Remove the second-to-last item from the array:             array.remove(-2);
// Remove the second and third items from the array:          array.remove(1, 2);
// Remove the last and second-to-last items from the array:   array.remove(-2, -1);
Array.prototype.remove = function(from, to) {
  var rest = this.slice((to || from) + 1 || this.length);
  this.length = from < 0 ? this.length + from : from;
  return this.push.apply(this, rest);
};