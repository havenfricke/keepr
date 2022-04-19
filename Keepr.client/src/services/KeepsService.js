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
}
export const keepsService = new KeepsService()