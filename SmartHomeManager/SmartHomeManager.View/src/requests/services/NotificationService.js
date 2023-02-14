import HttpService from "requests/HttpService";


const GET_ALL_NOTIFICATIONS_API_ROUTE = `https://localhost:7140/api/notification/all`
const GET_NOTIFICATIONS_API_ROUTE = `https://localhost:7140/api/notification/`
const POST_NOTIFICATION_API_ROUTE = "https://localhost:7140/api/notification"

function getAllNotifications() {
  return HttpService.get(GET_ALL_NOTIFICATIONS_API_ROUTE);
}

function getNotificationsByAccountId(accountId) {
  return HttpService.get(GET_NOTIFICATIONS_API_ROUTE + accountId)
} 

function sendNotification(body) {
  return HttpService.post(POST_NOTIFICATION_API_ROUTE, body)
}

export default {
  getAllNotifications, 
  getNotificationsByAccountId, 
  sendNotification
}