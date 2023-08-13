// const domain = 'http://api.techwarriors.click/'
const domain = 'https://localhost:7288/'
const routes = {
    GetAll: 'api/Article/GetList',
    post: 'api/Article/Insert',
    put: 'api/Article/Update',
    patch: 'api/Article/Delete',
    getDetails: 'api/Article/GetDetail',
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
    let url = domain + routes.patch + '?ArticleID=' + id
    await fetch(url, {
        method: 'patch'
    })
    window.location.reload()
}

const handleUpdate = async (id) => {
    console.log(id);
    let url = domain + routes.getDetails + '?ArticleID=' + id
    let response = await fetch(url, {method: 'get'})
                    .then(response => response.json())
    response = response.data
    console.log(response)
    var modal = new bootstrap.Modal(document.getElementById('updated_article'), {
        keyboard: false
    })
    modal.show()
    let formUpdate = document.getElementById('form-update-article')
    let title = formUpdate.querySelector('#title')
    let content = formUpdate.querySelector('#content')
    let image = formUpdate.querySelector('#image')
    let button = formUpdate.querySelector('#btn-update-article')
    title.value = response[0].title
    content.value = response[0].content
    button.addEventListener('click', async (e) => {
        e.preventDefault()
        let newTitle = title.value
        let newContent = content.value
        let urlImage = response[0].image
        console.log(urlImage);
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
            response = await response.json()
            urlImage = response.object
        }
        console.log(urlImage);
        console.log(newTitle);
        let updateData = {
            ArticleId: id,
            Title: newTitle,
            Content: newContent,
            Description: 'desc',
            Image: urlImage
        }
        let up = domain + routes.put
        console.log(JSON.stringify(updateData));
        await fetch(up, 
            {
                method: 'PUT', 
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(updateData)
            })
        modal.hide()
        window.location.reload()
    })
}

const GetListArticle = (data) => {
    let table = document.getElementById('table-article-list')
    let html = data.map((item, index) => {
        return `
            <tr>
                <th scope="row">${index + 1}</th>
                <td>${item.title}</td>
                <td>
                    <img src="${item.image}" alt="" width="50" height="50" class="rounded mx-auto">
                </td>
                <td>
                    ${validateDatetime(item.createDate)}
                </td>
                <td>
                    <button type="button" class="btn btn-warning"
                        onclick="handleDelete('${item.articleID}')"
                    >
                        <i class="ti ti-trash"></i>
                    </button>
                    <button type="button" class="btn btn-primary"
                        onclick="handleUpdate('${item.articleID}')"
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

const form = document.querySelector('#form-add-article')

form?.addEventListener('submit', (e) => handleSubmit(e))

const handleSubmit = async (e) => {
    e.preventDefault()
    const image = form.querySelector('#image').files[0]
    const title = form.querySelector('#title').value
    const content = form.querySelector('#content').value
    console.log(title , content);
    console.log(image);
    let formImage = new FormData()
    formImage.append('File', image, "IMAGE-TEST.JPG")
    console.log(formImage);
    let response = await fetch(`${domain}api/file/uploadfile`,
        {
            method: 'POST',
            body: formImage
        },
    )
    response = await response.json()
    console.log('image', response);
    let pathImage = response.object
    console.log(pathImage);
    let formData = new FormData()
    formData.append("Title", JSON.stringify(title))
    formData.append("Content", JSON.stringify(content))
    // formData.append('Image', JSON.stringify(pathImage))
    let data = JSON.stringify({
        Title: title,
        Content: content,
        Description: 'desc',
        Image: pathImage
    })
    response = await fetch(domain + routes.post, 
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: data
        })
    // window.location.href = '/article.html'
}
