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

  async getVaultKeeps(id) {
    const res = await api.get('/api/vaults/' + id + '/keeps')
    logger.log('getvaultkeeps', res.data)
    AppState.vaultkeeps = res.data
  }

  async editKeep(keepData) {
    const id = AppState.activeKeep.id
    const res = await api.put('/api/keeps/' + id, keepData)
    logger.log('editkeep', res.data)
    AppState.profilekeeps = AppState.profilekeeps.slice(k => k.id == id)
    AppState.profilekeeps = [...AppState.profilekeeps, res.data]
  }

  async removeKeep(id) {
    const res = api.delete('/api/keeps/' + id)
    logger.log('removekeep', res.data)
    AppState.profilekeeps = AppState.profilekeeps.filter(k => k.id != id)
  }

  async removeKV(id, vkid) {
    const res = await api.delete('/api/vaultkeeps/' + vkid)
    logger.log('removeKV', res.data)
    AppState.vaultkeeps = AppState.vaultkeeps.filter(vk => vk.id != id)

  }
}
export const keepsService = new KeepsService()