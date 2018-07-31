var pacman = {
    x: 1,
    y: 1,
};
var score = 0;
var world = [
    [2,2,2,2,2,2,2,2,2,2],
    [2,0,1,1,1,1,1,2,1,2],
    [2,2,2,2,1,2,1,1,1,2],
    [2,1,2,1,1,2,2,1,2,2],
    [2,1,2,1,1,1,1,1,1,2],
    [2,1,1,1,2,2,1,2,1,2],
    [2,1,2,1,1,1,1,2,1,2],
    [2,1,2,2,2,1,2,2,1,2],
    [2,1,1,1,1,1,1,1,1,2],
    [2,2,2,2,2,2,2,2,2,2],
];

function displayWorld(){
    var output = '';
    for(var i=0; i<world.length; i++){
        output += "<div class='row'>"
        for(var j=0; j<world[i].length; j++){
            if(i == pacman.x && j == pacman.y){
                output += "<div class='pacman'></div>"
                world[pacman.x][pacman.y] = 0;
            }
            else if(world[i][j] == 2){
                output += "<div class='brick'></div>"
            }
            else if(world[i][j] == 1){
                output += "<div class='coin'></div>"
            }
                else if(world[i][j] == 0){
                output += "<div class='empty'></div>"
            }
            else{
                console.log("NaN")
            }
        }
        output += "</div>"
    }
    // console.log(output)
    document.getElementById('world').innerHTML = output;
    // console.log(pacman)
}
function updateScore(){
    document.getElementById('score').innerHTML = score;
}

document.onkeydown = function(e){
    if(e.keyCode == 37 && world[pacman.x][pacman.y-1]!=2){
        pacman.y -= 1
    }
    else if(e.keyCode == 39 && world[pacman.x][pacman.y+1]!=2){
        pacman.y += 1
    }
    else if(e.keyCode == 38 && world[pacman.x-1][pacman.y]!=2){
        pacman.x -= 1
    }
    else if(e.keyCode == 40 && world[pacman.x+1][pacman.y]!=2){
        pacman.x += 1
    }
    if(world[pacman.x][pacman.y]==1){
        score += 1;
        world[pacman.x][pacman.y] = 0;
        updateScore();
    }
    displayWorld();
}
