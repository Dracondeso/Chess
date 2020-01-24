var start;
var check;
$(document).ready(function () {
    chessInitialize();
    $(".chess").click(function () {
        if ($("#" + this.id).hasClass("clicable")) {
            cleaner();
            start = this.id;
            pieceChosen(start);
        }
        if (($("#" + this.id).hasClass("movable")) || ($("#" + this.id).hasClass("eatable"))) {
            check = this.id;
            move(start, check);
        }
        if ($("#" + this.id).hasClass("castling")) {
            var kingPos = this.id;
            castlingMove(kingPos);
        }
    });
});
function chessInitialize() {
    $("#1-1").addClass("White WhiteRook clicable firstMove");
    $("#1-2").addClass("White WhiteKnight clicable firstMove")
    $("#1-3").addClass("White WhiteBishop clicable firstMove");
    $("#1-4").addClass("White WhiteKing clicable firstMove");
    $("#1-5").addClass("White WhiteQueen clicable firstMove");
    $("#1-6").addClass("White WhiteBishop clicable firstMove");
    $("#1-7").addClass("White WhiteKnight clicable firstMove");
    $("#1-8").addClass("White WhiteRook clicable firstMove");
    $("#2-1").addClass("White WhitePawn clicable firstMove");
    $("#2-2").addClass("White WhitePawn clicable firstMove");
    $("#2-3").addClass("White WhitePawn clicable firstMove");
    $("#2-4").addClass("White WhitePawn clicable firstMove");
    $("#2-5").addClass("White WhitePawn clicable firstMove");
    $("#2-6").addClass("White WhitePawn clicable firstMove");
    $("#2-7").addClass("White WhitePawn clicable firstMove");
    $("#2-8").addClass("White WhitePawn clicable firstMove");
    $("#7-1").addClass("Black BlackPawn clicable firstMove");
    $("#7-2").addClass("Black BlackPawn clicable firstMove");
    $("#7-3").addClass("Black BlackPawn clicable firstMove");
    $("#7-4").addClass("Black BlackPawn clicable firstMove");
    $("#7-5").addClass("Black BlackPawn clicable firstMove");
    $("#7-6").addClass("Black BlackPawn clicable firstMove");
    $("#7-7").addClass("Black BlackPawn clicable firstMove");
    $("#7-8").addClass("Black BlackPawn clicable firstMove");
    $("#8-1").addClass("Black BlackRook clicable firstMove");
    $("#8-2").addClass("Black BlackKnight clicable firstMove");
    $("#8-3").addClass("Black BlackBishop clicable firstMove");
    $("#8-4").addClass("Black BlackKing clicable firstMove");
    $("#8-5").addClass("Black BlackQueen clicable firstMove");
    $("#8-6").addClass("Black BlackBishop clicable firstMove");
    $("#8-7").addClass("Black BlackKnight clicable firstMove");
    $("#8-8").addClass("Black BlackRook clicable firstMove");
} //Place the chess pieces on board
function cleaner() {
    for (var i = 8; i > 0; i--) {
        for (var j = 1; j < 9; j++) {
            var position = i + "-" + j
            if ($("#" + position).hasClass("castling")) {
                $("#" + position).removeClass("castling");
            }
            if ($("#" + position).hasClass("clicked")) {
                $("#" + position).removeClass("clicked");
                $("#" + position).addClass("clicable");


            }
            if ($("#" + position).hasClass("movable")) {
                $("#" + position).removeClass("movable");

            }
            if ($("#" + position).hasClass("eatable")) {
                $("#" + position).removeClass("eatable");
                $("#" + position).addClass("clicable");
            }
        }
    }
}//clean assigned class
function pieceChosen(start) {
    $("#" + start).removeClass("clicable");
    $("#" + start).addClass("clicked");

    var pieceType = $("#" + start)[0].classList[3]
    switch (pieceType) {
        case 'BlackPawn':
            pawnMove(start);
            break;
        case 'WhitePawn':
            pawnMove(start);
            break;
        case 'WhiteBishop':
            bishopMove(start);
            break;
        case 'BlackBishop':
            bishopMove(start);
            break;
        case 'WhiteKnight':
            knightMove(start);
            break;
        case 'BlackKnight':
            knightMove(start);
            break;
        case 'WhiteRook':
            rookMove(start);
            break;
        case 'BlackRook':
            rookMove(start);
            break;
        case 'WhiteQueen':
            queenMove(start);
            break;
        case 'BlackQueen':
            queenMove(start);
            break;
        case 'WhiteKing':
            kingMove(start);
            break;
        case 'BlackKing':
            kingMove(start);
            break;
        default:
            break;
    }
}//find the piece on box clicked
function pawnMove(start) {
    if ($("#" + start).hasClass("White")) {
        north(start);
        northEast(start);
        northWest(start);
    }
    else {
        south(start);
        southEast(start);
        southWest(start);
    }
}//Rule for pawn move
function bishopMove(start) {
    northEast(start);
    northWest(start);
    southEast(start);
    southWest(start);
}//Rule for bishop move
function rookMove(start) {
    north(start);
    south(start);
    east(start);
    west(start);
}//Rule for rook move
function knightMove(start) {
    var position = start.split('-');
    if ((parseInt(position[0]) < 7)) {
        if ((parseInt(position[1])) < 8) {
            statAssigner(((parseInt(position[0]) + 2) + "-" + (parseInt(position[1]) + 1)), start);
        }
        if ((parseInt(position[1])) > 1) {
            statAssigner(((parseInt(position[0]) + 2) + "-" + (parseInt(position[1]) - 1)), start);
        }
    }

    if ((parseInt(position[0]) > 2)) {
        if ((parseInt(position[1])) < 8) {
            statAssigner(((parseInt(position[0]) - 2) + "-" + (parseInt(position[1]) + 1)), start);
        }
        if ((parseInt(position[1])) > 1) {
            statAssigner(((parseInt(position[0]) - 2) + "-" + (parseInt(position[1]) - 1)), start);
        }
    }
    if ((parseInt(position[1]) < 7)) {
        if ((parseInt(position[0])) < 8) {
            statAssigner(((parseInt(position[0]) + 1) + "-" + (parseInt(position[1]) + 2)), start);
        }
        if ((parseInt(position[0])) > 1) {
            statAssigner(((parseInt(position[0]) - 1) + "-" + (parseInt(position[1]) + 2)), start);
        }
    }
    if ((parseInt(position[1]) > 2)) {
        if ((parseInt(position[0])) < 8) {
            statAssigner(((parseInt(position[0]) + 1) + "-" + (parseInt(position[1]) - 2)), start);
        }
        if ((parseInt(position[0])) > 1) {
            statAssigner(((parseInt(position[0]) - 1) + "-" + (parseInt(position[1]) - 2)), start);
        }
    }
}//Rule for knight move
function queenMove(start) {
    bishopMove(start);
    rookMove(start);

}//Rule for queen move
function kingMove(start) {
    queenMove(start);
    castling(start);
}//Rule for king move
function north(start) {
    var position = start.split("-");
    var j = 8 - (parseInt(position[0]));
    if ($("#" + start).hasClass("WhitePawn") || $("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    if ($("#" + start).hasClass("WhitePawn firstMove")) {
        j = 2;
    }
    for (i = 1; i <= j; i++) {
        var check = (parseInt(position[0]) + i) + "-" + (parseInt(position[1]));
        if ($("#" + check).hasClass("clicable")) {
            i = 8;
        }
        statAssigner(check, start);
    }
}//find box to north
function south(start) {
    var position = start.split("-");
    var j = (parseInt(position[0]));
    if ($("#" + start).hasClass("BlackPawn") || $("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    if ($("#" + start).hasClass("BlackPawn firstMove")) {
        j = 2;
    }

    for (i = 1; i <= j; i++) {
        var check = (parseInt(position[0]) - i) + "-" + (parseInt(position[1]));
        if ($("#" + check).hasClass("clicable")) {
            i = j;
        }
        statAssigner(check, start);
    }
}//find box to south
function east(start) {
    var position = start.split("-");
    var j = 8 - (parseInt(position[1]));
    if ($("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }

    for (i = 1; i <= j; i++) {
        var check = (parseInt(position[0])) + "-" + (parseInt(position[1]) + i);
        if ($("#" + check).hasClass("clicable")) {
            i = 8;
        }
        statAssigner(check, start);
    }
}//find box to east
function west(start) {
    var position = start.split("-");
    var j = (parseInt(position[1]));
    if ($("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    for (i = 1; i <= j; i++) {
        var check = (parseInt(position[0])) + "-" + (parseInt(position[1] - i));
        if ($("#" + check).hasClass("clicable")) {
            i = j;
        }
        statAssigner(check, start);
    }
}//find box to west
function northEast(start) {
    var position = start.split("-");
    var j = 8 - (parseInt(position[0]));
    var check = (parseInt(position[0]) + 1) + "-" + (parseInt(position[1]) + 1);
    if (($("#" + start).hasClass("WhitePawn") && $("#" + check).hasClass("clicable") && $("#" + start)[0].classList[2] != $("#" + check)[0].classList[2]) || $("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    if ($("#" + start).hasClass("WhitePawn") && !($("#" + check).hasClass("clicable"))) {
        j = 0;
    }
    for (i = 1; i <= j; i++) {
        check = (parseInt(position[0]) + i) + "-" + (parseInt(position[1]) + i);
        if ($("#" + check).hasClass("clicable")) {
            i = 8;
        }
        statAssigner(check, start);
    }
}//find box to north east
function northWest(start) {
    var position = start.split("-");
    var j = 8 - (parseInt(position[0]));
    var check = (parseInt(position[0]) + 1) + "-" + (parseInt(position[1]) - 1);
    if (($("#" + start).hasClass("WhitePawn") && $("#" + check).hasClass("clicable") && $("#" + start)[0].classList[2] != $("#" + check)[0].classList[2]) || $("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    if ($("#" + start).hasClass("WhitePawn") && !($("#" + check).hasClass("clicable"))) {
        j = 0;
    }
    for (i = 1; i <= j; i++) {
        check = (parseInt(position[0]) + i) + "-" + (parseInt(position[1]) - i);
        if ($("#" + check).hasClass("clicable")) {
            i = 8;
        }
        statAssigner(check, start);
    }
}//find box to north west
function southEast(start) {
    var position = start.split("-");
    var j = (parseInt(position[0]));
    var check = (parseInt(position[0]) - 1) + "-" + (parseInt(position[1]) + 1);
    if (($("#" + start).hasClass("BlackPawn") && $("#" + check).hasClass("clicable") && $("#" + start)[0].classList[2] != $("#" + check)[0].classList[2]) || $("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    if ($("#" + start).hasClass("BlackPawn") && !($("#" + check).hasClass("clicable"))) {
        j = 0;
    }
    for (i = 1; i <= j; i++) {
        check = (parseInt(position[0]) - i) + "-" + (parseInt(position[1]) + i);
        if ($("#" + check).hasClass("clicable")) {
            i = 8;
        }
        statAssigner(check, start);
    }
}//find box to south east
function southWest(start) {
    var position = start.split("-");
    var j = (parseInt(position[0]));
    var check = (parseInt(position[0]) - 1) + "-" + (parseInt(position[1]) - 1);
    if (($("#" + start).hasClass("BlackPawn") && $("#" + check).hasClass("clicable") && $("#" + start)[0].classList[2] != $("#" + check)[0].classList[2]) || $("#" + start).hasClass("WhiteKing") || $("#" + start).hasClass("BlackKing")) {
        j = 1;
    }
    if ($("#" + start).hasClass("BlackPawn") && !($("#" + check).hasClass("clicable"))) {
        j = 0;
    }
    for (i = 1; i <= j; i++) {
        check = (parseInt(position[0]) - i) + "-" + (parseInt(position[1]) - i);
        if ($("#" + check).hasClass("clicable")) {
            i = 8;
        }
        statAssigner(check, start);
    }
}//find box to south west
function castling(start) {
    if ($("#" + start).hasClass("firstMove")) {
        var position = start.split("-");
        var check1 = parseInt(position[0]) + "-" + (parseInt(position[1]) + 1);
        var check2 = parseInt(position[0]) + "-" + (parseInt(position[1]) + 2);
        var check3 = parseInt(position[0]) + "-" + (parseInt(position[1]) + 3);
        var check4 = parseInt(position[0]) + "-" + (parseInt(position[1]) + 4);
        var check5 = parseInt(position[0]) + "-" + (parseInt(position[1]) - 1);
        var check6 = parseInt(position[0]) + "-" + (parseInt(position[1]) - 2);
        var check7 = parseInt(position[0]) + "-" + (parseInt(position[1]) - 3);
        if ($("#" + check4).hasClass("firstMove") && !($("#" + check1).hasClass("clicable")) && !($("#" + check2).hasClass("clicable")) && !($("#" + check3).hasClass("clicable"))) {
            $("#" + check3).addClass("castling");
        }
        if ($("#" + check7).hasClass("firstMove") && !($("#" + check5).hasClass("clicable")) && !($("#" + check6).hasClass("clicable"))) {
            $("#" + check6).addClass("castling");
        }
    }
}//find box if castling is possible
function castlingMove(kingPos) {
    const clean = ['castling', 'firstMove', 'movable', 'clicked', 'clicable', 'White', 'Black', 'WhitePawn', 'BlackPawn', 'WhiteBishop', 'BlackBishop', 'WhiteKnight', 'BlackKnight', 'WhiteRook', 'BlackRook', 'WhiteQueen', 'BlackQueen', 'WhiteKing', 'BlackKing'];
    var rookPos;
    var rookPiece;
    var rookNewPos;
    var color = $("#" + (parseInt(kingPos.split("-")[0])) + "-4")[0].classList[2];
    var kingPiece = $("#" + (parseInt(kingPos.split("-")[0])) + "-4")[0].classList[3];
    if (parseInt(kingPos.split("-")[1]) == 2) {
        rookPos = (parseInt(kingPos.split("-")[0])) + "-" + (parseInt(kingPos.split("-")[1]) - 1);
        rookNewPos = (parseInt(kingPos.split("-")[0]) + "-" + (parseInt(kingPos.split("-")[1]) + 1));
        rookPiece = $("#" + rookPos)[0].classList[3];
        $("#" + rookPos).removeClass(clean);

    }
    if (parseInt(kingPos.split("-")[1]) == 7) {
        rookPos = (parseInt(kingPos.split("-")[0]) + "-" + (parseInt(kingPos.split("-")[1]) + 1));
        rookPiece = $("#" + rookPos)[0].classList[3];

        rookNewPos = (parseInt(kingPos.split("-")[0]) + "-" + (parseInt(kingPos.split("-")[1]) - 1));
        $("#" + rookPos).removeClass(clean);
    }
    $("#" + (parseInt((kingPos.split("-")[0]))) + "-4").removeClass(clean);
    $("#" + kingPos).removeClass(clean);
    $("#" + kingPos).addClass(color);
    $("#" + kingPos).addClass(kingPiece);
    $("#" + kingPos).addClass('clicable');
    $("#" + rookNewPos).addClass(color);
    $("#" + rookNewPos).addClass(rookPiece);
    $("#" + rookNewPos).addClass('clicable');
    cleaner();
}//assign and remove classes for castling
function statAssigner(check, start) {
    var first = start.split("-");
    var second = check.split("-");
    if ((1 < first[0] < 7) && (1 < first[1] < 7)) {
        if (!($("#" + check).hasClass("clicable"))) {
            $("#" + check).addClass("movable");
        }
        if ((!(((((first[0] == (parseInt(second[0]) - 1)) || (first[0] == (parseInt(second[0]) + 1))) && (first[1] == (parseInt(second[1])))) || (first[0] == (parseInt(second[0]) - 2)) || (first[0] == (parseInt(second[0]) + 2))) && (($("#" + start)[0].classList[3] == "WhitePawn") || ($("#" + start)[0].classList[3] == "BlackPawn"))) && ($("#" + check).hasClass("clicable")) && ($("#" + check)[0].classList[2] != $("#" + start)[0].classList[2]))) {
            if (!(($("#" + check).hasClass("WhiteKing")) || $("#" + check).hasClass("BlackKing"))) {
                $("#" + check).removeClass("clicable");
                $("#" + check).addClass("eatable");
            }
            else {
                console.log("scacco")
            }

        }
    }
}//assign if is movable or eatable
function move(start, check) {

    const clean = ['castling', 'firstMove', 'movable', 'clicked', 'clicable', 'White', 'Black', 'WhitePawn', 'BlackPawn', 'WhiteBishop', 'BlackBishop', 'WhiteKnight', 'BlackKnight', 'WhiteRook', 'BlackRook', 'WhiteQueen', 'BlackQueen', 'WhiteKing', 'BlackKing'];
    var classe2 = $("#" + start)[0].classList[2];
    var classe3 = $("#" + start)[0].classList[3];
    $("#" + check).removeClass(clean);
    $("#" + check).addClass(classe2);
    $("#" + check).addClass(classe3);
    $("#" + check).addClass('clicable');
    $("#" + start).removeClass(clean);
    cleaner();
    sendMove(start, check)
}//assign and remove classes for movement
function sendMove(start, check) {

    var json = {
        start: start,
        end: check
    };
    JSON.stringify(json, false, 4);
}
function setCookie(userName, password, H_expiredTime) {
    var d = new Date();
    d.setTime(d.getTime() + (H_expiredTime * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = userName + ";" + password + ";" + expires;
}
