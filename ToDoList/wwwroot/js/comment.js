window.addEventListener('load', function () {

    var id = document.querySelector('.carD');

    fetch("/Main/AllComments", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(id.id)
    })
        .then(response => response.json())
        .then(data => {
            if (data != null) {
                var chat = document.querySelector('.chat-window');
                var i = 0;
                data.comment.forEach(element => {
                    var h3 = document.createElement('h3');
                    h3.textContent = data.user[i].name;
                    i++;
                    var h4 = document.createElement('h4');
                    h4.textContent = element.text;
                    var mes = document.createElement('div');
                    mes.className = 'message';
                    mes.appendChild(h3);
                    mes.appendChild(h4);
                    chat.appendChild(mes);

                })
            }
        });

});


var send = document.querySelector('.send-button');
send.addEventListener('click', function () {
    var input = document.querySelector('.message-input');
    if (input.value !== '') {
        var idtask = document.querySelector('.carD');
        var iduser = document.querySelector('.container');
        
        var data = {
            iduser: iduser.id,
            idtask: idtask.id,
            text: input.value
        };
        fetch("/Main/AddComments", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
            .then(response => response.json())
            .then(data => {
                if (data != null) {

                    var chat = document.querySelector('.chat-window');


                    var h3 = document.createElement('h3');
                    h3.textContent = data.users.name;
                    var h4 = document.createElement('h4');
                    h4.textContent = data.comment.text;
                    var mes = document.createElement('div');
                    mes.className = 'message';
                    mes.appendChild(h3);
                    mes.appendChild(h4);
                    chat.appendChild(mes);
                    input.value = '';
                }

             })
    }
});
