const studentsUri = "api/students/";

let templates = {
    "get": "Get",
    "post": "Post",
    "put": "Put",
    "delete": "Delete",
    "get_paginated": "GetPaginated",
    "get_filtered": "GetFiltered",
    "get_filtered_paginated": "GetFilteredAndPaginated"
}

const methods = Object.keys(templates).map(key => {
    return { [key]: studentsUri + templates[key] }
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
    let id = e.target.id;

}

function onCheckBoxClick(element) {
    let id = element.id;
    let div = document.querySelector(`div #${id}`).firstChild;
    div.style.display = element.checked ? "inherited" : "none";
}
