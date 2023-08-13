// const domain = 'http://api.techwarriors.click/'
const domain = 'https://localhost:7288/'
const routes = {
    GetAll: 'api/category/GetList',
    post: 'api/category/Insert',
    put: 'api/category/Update',
    patch: 'api/category/Delete',
    getDetails: 'api/category/GetDetail',
}
const fetchData = async (url, method, callback, data) => {

    let response = await fetch(url, 
    {
        method: method ?? 'GET',
    },
    data ? JSON.stringify(data) : ''
    )
    response  = await response.json()
    callback ? callback(response.data) : ''
    console.log(response)
};

const handleDelete = async (id) => {
    console.log(id);
    let url = domain + routes.patch + '?CategoryId=' + id
    let res = await fetch(url, {
        method: 'patch'
    })
    console.log(res);
    if (res.ok) {
        window.location.reload()
    }
}

const handleUpdate = async (id) => {
    console.log(id);
    let url = domain + routes.getDetails + '?CategoryId=' + id
    let response = await fetch(url, {method: 'get'})
                    .then(response => response.json())
    response = response.data
    console.log(response)
    var modal = new bootstrap.Modal(document.getElementById('updated_category'), {
        keyboard: false
    })
    modal.show()
    let formUpdate = document.getElementById('form-update-category')
    let name = formUpdate.querySelector('#CategoryName')
    let country = formUpdate.querySelector('#CategoryParent')
    let button = formUpdate.querySelector('#btn-update-category')
    name.value = response[0].categoryName
    let options = Array.from(country.options)
    let optionToSelect = options.find(item => item.value === response[0].categoryID);
    if (optionToSelect) {
        optionToSelect.selected = true;
    }
    button.addEventListener('click', async (e) => {
        e.preventDefault()
        let newName = name.value
        let newCountry = country.value
        
        let updateData = {
            categoryID: id,
            categoryName: newName,
            parentID: newCountry,
        }
        let up = domain + routes.put
        console.log(JSON.stringify(updateData));
        const res = await fetch(up, 
            {
                method: 'PUT', 
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(updateData)
            })
        console.log(res);
        if (res.ok) {
            modal.hide()
            window.location.reload()
        }
    })
}

const GetListArticle = (data) => {
    let table = document.getElementById('table-category-list')
    let html = data.map((item, index) => {
        return `
            <tr>
                <th scope="row">${index + 1}</th>
                <td>${item.categoryName}</td>
                <td>
                    ${validateDatetime(item.createDate)}
                </td>
                <td>
                    <button type="button" class="btn btn-warning"
                        onclick="handleDelete('${item.categoryID}')"
                    >
                        <i class="ti ti-trash"></i>
                    </button>
                    <button type="button" class="btn btn-primary"
                        onclick="handleUpdate('${item.categoryID}')"
                    >
                        <i class="ti ti-settings"></i>
                    </button>
                </td>
            </tr>
        `
    })
    // console.log(table.hasChildNodes())
    // console.log(table.firstElementChild)
    // if (table) {
    //     // while (table.hasChildNodes()) {
    //     //     // table.removeChild(table.hasChildNodes());
    //     // }
    // }
    if (table) {
        table.innerHTML = html.join('')
    }
}

const GetListSelect = (data) => {
    const select = document.getElementById('CategoryParent')
    let html = data.map(item => {
        return (
            `<option value="${item.categoryID}">${item.categoryName}</option>`
        )
    })
    select.innerHTML = html.join('')
}
const validateDatetime = (datetimeString) => {
    // Kiểm tra định dạng ngày và giờ (datetime) hợp lệ
    // Định dạng mẫu: YYYY-MM-DDTHH:MM:SS.sss
  
    // var regex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}$/;
  
    // if (!regex.test(datetimeString)) {
    //   // Định dạng không hợp lệ
    //   return false;
    // }
  
    // Kiểm tra tính hợp lệ của ngày và giờ
    var datetime = new Date(datetimeString);
  
    if (isNaN(datetime.getTime())) {
      // Ngày và giờ không hợp lệ
      return false;
    }
    
    // Trích xuất thông tin ngày và giờ
    var month = datetime.getMonth() + 1; // Lấy tháng tính từ 0, nên cần cộng thêm 1
    var hours = datetime.getHours();
    var minutes = datetime.getMinutes();
    var day = datetime.getDate();
    var year = datetime.getFullYear();
  
    // Định dạng lại chuỗi ngày và giờ
    var formattedDatetime = `${padZero(month)}:${padZero(hours)} / ${padZero(day)}-${padZero(month)}-${year}`;
  
    // Trả về định dạng đã định kỳ
    return formattedDatetime;
}
const padZero = (number) => {
    return number.toString().padStart(2, '0');
}
// GetListArticle()
// http://api.techwarriors.click/api/Article/GetList?PageSize=10&CurrentPage=1
fetchData(`${domain + routes.GetAll}?PageSize=10&CurrentPage=1`, 'GET', GetListArticle)

fetchData(`${domain + routes.GetAll}?PageSize=10&CurrentPage=1`, 'GET', GetListSelect)


const form = document.querySelector('#form-add-club')

form?.addEventListener('submit', (e) => handleSubmit(e))

const handleSubmit = async (e) => {
    e.preventDefault()
    const name = form.querySelector('#CategoryName').value
    const parent = form.querySelector('#CategoryParent').value
    let data = JSON.stringify({
        categoryName: name,
        parentID: parent
    })
    response = await fetch(domain + routes.post, 
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: data
        })
    console.log(response)
    if (response.ok) {
        window.location.replace('./category.html')
    }
}
