 $(document).ready(function()
 {      
      var serviceURL = "http://localhost:1429/api/connect-four";   
      /*Board*/
      function setTable(columns, rows)
      {   
            var board =  $('#tableBoard');

            if(board != undefined){                  
                  board.hide();
            }

            var table_body ='<table id="board">';
            for(var i=0;i<rows;i++)
            {
                  table_body+='<tr>';
                  for(var j=0;j<columns;j++)
                  {  
                        table_body +='<td class="hole">';
                        table_body +=' ';
                        table_body +='</td>';
                  }
                  table_body+='</tr>';
            }

            table_body+='</table>';
            board.html(table_body);           
            board.fadeIn('slow');            
      }      

      function setSizeBoard(columns, rows)
      {
            localStorage.setItem("board_width", columns);
            localStorage.setItem("board_height", rows);      
      }
     
      function activeButtons(){            
            var winnerButton = $("#show_winner");            
            winnerButton.removeAttr('disabled');            
            winnerButton.css("cursor", "pointer");            
            winnerButton.css("background", "#ccc");            
            winnerButton.css("color", "#333");
      } 

      function radioClick(columns, rows)
      {
            setTable(columns,rows);
            setSizeBoard(columns,rows);  
            activeButtons();
             if($("#winnerText") != undefined)
                  $("#winnerText").remove();   
      }


      function cleanMessages()
      {
            if($("#winnerText") != undefined)
                  $("#winnerText").remove();

            if($("#errorText") != undefined)
                  $("#errorText").remove(); 

            if($("#wayText") != undefined)
                  $("#wayText").remove();  
      }


      $("#radio-1").click(function(){            
            radioClick(7, 6);
      });   

      $("#radio-2").click(function(){                        
            radioClick(5, 4);
      });           

      $("#radio-3").click(function(){            
            radioClick(9, 7);
      });

      function getWinner(inputValue,widthValue,heightValue)
      {     
            cleanMessages();

            $.ajax({
                  url: serviceURL,
                  type: 'GET',
                  data: {
                     input:inputValue,
                     width: widthValue,
                     height: heightValue
                  },
                  success: function(response)
                  {     
                        paintSolution(response);
                  },
                  error: function(response)
                  { 
                        var messages = response.responseJSON.userMessage.split('.');
                     
                        $("#errors").html('<div id="errorText"></div>');  
                        messages.forEach((element, index) => {                                                                                 
                              if(element != ''){
                                    $("#errorText").append('<p>'+ element +'</p>');
                              }
                        })
                  }                  
            });                
      }

     function setWinnerText(winner)
     {
            var result = '';

            if(winner == 'A_Winner')
            {
                  result = 'And the winner is...Team A!!!';
                  $("#winner").css("color","blue");

            } if(winner == 'B_Winner')
            {
                  result = 'And the winner is...Team B!!!';
                  $("#winner").css("color","red");

            }else if(winner == 'Ongoing'){
                  result = 'The game is ongoing';
                  $("#winner").css("color","black");

            }else if(winner == 'Tie')
            {
                  result = 'Nobody wins';
                  $("#winner").css("color","gray");
            }

            $("#winner").html('<p id="winnerText">'+result+'</p>');
      }

      function setWayText(way)
      {
            var result = '';

            if(way == 'Horizontally')
            {
                  result = way;                

            } if(way == 'Vertically')
            {
                  result = way;                 

            }else if(way == 'DiagonallyUpForward')
            {
                  result = 'Diagonally Up Forward';                  

            }else if(way == 'DiagonallyUpBackward ')
            {
                  result = 'Diagonally Up Backward';                  
            }

            $("#way").html('<p id="wayText">'+result+'</p>');
      }

      function paintSolution(response){

            var tooltip =  response.board;                                   
            var width = localStorage.getItem("board_width");
            var height = localStorage.getItem("board_height");   
            
            setWinnerText(response.solutionType);
            setWayText(response.winType);
            
            var table = document.getElementsByTagName("table");            
            $("#board").attr('title', tooltip);

            var rows = document.getElementsByTagName("tr");
            
            for(var i = 0; i < height; i++) 
            {
                  var row = rows[height-i-1];
                  var columns = row.getElementsByTagName("td");
                  for (var j = 0; j < width; j++) 
                  { 
                        var col = columns[j];                                                
                        var teamPiece = response.boardData[i][j];                        

                        if(teamPiece == 'A')
                        {                                   
                            col.style.background = "blue";
                            
                        }else if(teamPiece == 'B')
                        {  
                            col.style.background = "red";
                        }
                  }  
            }
      }

      $("#formConnect").submit(function(e)
      {
            e.preventDefault();
            var input = $('input[name="inputText"]').val(); 

            if(input == undefined || input == '' ){                  
                  cleanMessages();
                   $("#errors").html('<div id="errorText"><p>Input required</p></div>');                          
            }else
            {
                  var width = localStorage.getItem("board_width");     
                  var height = localStorage.getItem("board_height");   
                  setTable(width, height);
                  getWinner(input,width,height);
            }            
      });


      $( function() {
            $( document ).tooltip({
             track: true
            });
      });

      /*Radio button selector*/           
      $("input[type='radio']").checkboxradio();

      	
  });