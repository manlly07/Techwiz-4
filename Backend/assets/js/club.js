// const domain = 'http://api.techwarriors.click/'
const domain = 'https://localhost:7288/'
const routes = {
    GetAll: 'api/club/GetList',
    post: 'api/club/Insert',
    put: 'api/club/Update',
    patch: 'api/club/Delete',
    getDetails: 'api/club/GetDetail',
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
    let url = domain + routes.patch + '?clubId=' + id
    await fetch(url, {
        method: 'patch'
    })
    window.location.reload()
}

const handleUpdate = async (id) => {
    console.log(id);
    let url = domain + routes.getDetails + '?ClubId=' + id
    let response = await fetch(url, {method: 'get'})
                    .then(response => response.json())
    response = response.data
    console.log(response)
    var modal = new bootstrap.Modal(document.getElementById('updated_club'), {
        keyboard: false
    })
    modal.show()
    let formUpdate = document.getElementById('form-update-club')
    let name = formUpdate.querySelector('#ClubName')
    let country = formUpdate.querySelector('#Country')
    let image = formUpdate.querySelector('#image')
    let founding = formUpdate.querySelector('#Founding')
    let button = formUpdate.querySelector('#btn-update-club')
    name.value = response[0].clubName
    let options = Array.from(country.options)
    let optionToSelect = options.find(item => item.value === response[0].countryID);
    if (optionToSelect) {
        optionToSelect.selected = true;
    }
    button.addEventListener('click', async (e) => {
        e.preventDefault()
        let newName = name.value
        let newCountry = country.value
        let urlImage = response[0].image || response[0].logo
        console.log(urlImage);
        let newFounding = founding.value || response[0].founding
        if (image.files[0]) {
            let formImage = new FormData()
            formImage.append('File', image.files[0], "IMAGE-TEST.JPG")
            console.log(formImage);
            let response = await fetch(`${domain}api/file/uploadfile`,
                {
                    method: 'POST',
                    body: formImage
                },
            )
            response = await response.text()
            urlImage = response
        }
        console.log(urlImage);
        // console.log(newTitle);
        let updateData = {
            clubID: id,
            clubName: newName,
            countryID: newCountry,
            logo: urlImage,
            founding: newFounding,
            description: 'desc'
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
    let table = document.getElementById('table-club-list')
    let html = data.map((item, index) => {
        return `
            <tr>
                <th scope="row">${index + 1}</th>
                <td>${item.clubName}</td>
                <td>
                    <img src="${item.logo}" alt="" width="50" height="50" class="rounded mx-auto">
                </td>
                <td>
                    ${validateDatetime(item.founding)}
                </td>
                <td>
                    ${item.countryID}
                </td>
                <td>
                    <button type="button" class="btn btn-warning"
                        onclick="handleDelete('${item.clubID}')"
                    >
                        <i class="ti ti-trash"></i>
                    </button>
                    <button type="button" class="btn btn-primary"
                        onclick="handleUpdate('${item.clubID}')"
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
    const select = document.getElementById('Country')
    let html = data.map(item => {
        return (
            `<option value="${item.countryID}">${item.countryName}</option>`
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

fetchData(`${domain}api/country/getlist?PageSize=100&CurrentPage=1`, 'GET', GetListSelect)

const form = document.querySelector('#form-add-club')

form?.addEventListener('submit', (e) => handleSubmit(e))

const handleSubmit = async (e) => {
    e.preventDefault()
    const image = form.querySelector('#image').files[0]
    const name = form.querySelector('#ClubName').value
    const country = form.querySelector('#Country').value
    const description = form.querySelector('#Description').value
    const founding = form.querySelector('#Founding').value
    let formImage = new FormData()
    formImage.append('File', image, "IMAGE-TEST.JPG")
    console.log(formImage);
    let response = await fetch(`${domain}api/file/uploadfile`,
        {
            method: 'POST',
            body: formImage
        },
    )
    // console.log(response.body)
    response = await response.text()
    console.log('image', response);
    let pathImage = response
    console.log(pathImage);
    let data = JSON.stringify({
        clubName: name,
        countryID: country,
        description: description,
        logo: pathImage,
        founding: founding
    })
    response = await fetch(domain + routes.post, 
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: data
        })
    console.log(response)
    if (response.ok) {
        window.location.replace('./club.html')

    }
}
