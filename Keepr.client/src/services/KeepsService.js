import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

  async getAllKeeps() {
    const res = await api.get('/api/keeps')
    logger.log('getallkeeps', res.data)
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    const res = await api.get("/api/keeps/" + id)
    logger.log('getkeepbyid', res.data)
    AppState.activeKeep = res.data
    logger.log(AppState.activeKeep)
  }

  async getUserKeeps(id) {
    id = AppState.account.id
    const res = await api.get('/api/profiles/' + id + '/keeps')
    logger.log('getuserkeeps', res.data)
    AppState.profilekeeps = res.data
  }

  async createKeep(keepData) {
    const res = await api.post('/api/keeps/', keepData)
    logger.log('createkeep', res.data)
    AppState.profilekeeps = [...AppState.profilekeeps, res.data]
  }
}
export const keepsService = new KeepsService()