document.onreadystatechange = () => {
    document.querySelectorAll("button").forEach(element => {
        element.onclick = onButtonClick;
    })
}

const studentsUri = "api/students/";

let methods = {
    "get": "Get",
    "post": "Post",
    "put": "Put",
    "delete": "Delete",
    "get_paginated": "GetPaginated",
    "get_filtered": "GetFiltered",
    "get_filtered_paginated": "GetFilteredAndPaginated"
}

Object.keys(methods).forEach(key => {
    methods[key] = studentsUri + methods[key];
});

const handlers = {
    "get": readStudent,
    "post": createStudent,
    "put": updateStudent,
    "delete": deleteStudent,
    "get_paginated": readPagedStudents,
    "get_filtered": readFilteredStudents,
    "get_filtered_paginated": readFilteredPaginatedStudents
}

function onButtonClick(e) {
    let id = $(e.target).prev().attr("id");
    let handler = handlers[id];
    handler(id);
}

function readStudent(id) {
    let url = methods[id];
    let stId = $(`#${id} input[name='Id']`).val();
    let fields = $(`#${id} input[name='Fields']`).val();
    if (fields) {
        url = url + `?fields=${fields}`;
    } else {
        url: url + `?id=${stId ? stId : 0}`;
    }
    $.ajax({
        method: "GET",
        url
    })
    .done(data => {
        if (data.StatusCode == "400") {
            $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
        } else {
            $("#result").html(JSON.stringify(data));
        }
    })
}
function createStudent(id) {
    let url = methods[id];
    let name = $(`#${id} input[name='Name']`).val();
    let phone = $(`#${id} input[name='Phone']`).val();
    $.ajax({
        method: "POST",
        url,
        data: {
            "Name": name,
            "Phone": phone
        }
    })
        .done(data => {
            if (data.StatusCode == "400") {
                $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
            } else {
                $("#result").html(JSON.stringify(data));
            }
    })
}
function updateStudent(id) {
    let url = methods[id];
    let stId = $(`#${id} input[name='Id']`).val();
    let name = $(`#${id} input[name='Name']`).val();
    let phone = $(`#${id} input[name='Phone']`).val();
    debugger
    $.ajax({
        method: "PUT",
        url: url + "?id=" + stId,
        data: {
            "Name": name == "" ? null : name,
            "Phone": phone == "" ? null : phone
        }
    })
        .done(data => {
            if (data.StatusCode == "400") {
                $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
            } else {
                $("#result").html(JSON.stringify(data));
            }
        })
}
function deleteStudent(id) {
    let url = methods[id];
    let stId = $(`#${id} input[name='Id']`).val();
    $.ajax({
        method: "Delete",
        url: url + `?id=${stId ? stId : 0}`
    })
        .done(data => {
            if (data.StatusCode == "400") {
                $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
            } else {
                $("#result").html(JSON.stringify(data));
            }
        })
}
function readPagedStudents(id) {
    let url = methods[id];
    let offset = parseInt($(`#${id} input[name='Offset']`).val());
    let limit = parseInt($(`#${id} input[name='Limit']`).val());
    let sort = $(`#${id} input[name='SortField']:checked`).val() == "true";
    $.ajax({
        method: "POST",
        url,
        data: {
            "Offset": offset,
            "Limit": limit,
            "Sort": sort
        }
    })
        .done(data => {
            if (data.StatusCode == "400") {
                $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
            } else {
                $("#result").html(JSON.stringify(data));
            }
        })
}
function readFilteredStudents(id) {
    let url = methods[id];
    let minId = parseInt($(`#${id} input[name='MinId']`).val());
    let maxId = parseInt($(`#${id} input[name='MaxId']`).val());
    let like = $(`#${id} input[name='Like']`).val();
    let globalLike = $(`#${id} input[name='GlobalLike']`).val();
    $.ajax({
        method: "POST",
        url,
        data: {
            "MinId": minId,
            "MaxId": maxId,
            "Like": like,
            "GlobalLike": globalLike
        }
    })
        .done(data => {
            if (data.StatusCode == "400") {
                $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
            } else {
                $("#result").html(JSON.stringify(data));
            }
        })
}
function readFilteredPaginatedStudents(id) {
    let url = methods[id];
    let offset = parseInt($(`input[name='Offset']`).val());
    let limit = parseInt($(`input[name='Limit']`).val());
    let sort = $(`input[name='SortField']:checked`).val() == "true";
    let minId = parseInt($(`input[name='MinId']`).val());
    let maxId = parseInt($(`input[name='MaxId']`).val());
    let like = $(`input[name='Like']`).val();
    let globalLike = $(`#input[name='GlobalLike']`).val();
    $.ajax({
        method: "POST",
        url,
        data: {
            "Filter": {
                "MinId": minId,
                "MaxId": maxId,
                "Like": like,
                "GlobalLike": globalLike
            },
            "Pagination": {
                "Offset": offset,
                "Limit": limit,
                "Sort": sort
            }
        }
    })
        .done(data => {
            if (data.StatusCode == "400") {
                $("#result").html(`<a href='${data.Info}'>Получить информацию об ошибке</a>`)
            } else {
                $("#result").html(JSON.stringify(data));
            }
        })
}