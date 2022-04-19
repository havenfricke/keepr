import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async getMyVaults(id) {
    id = AppState.account.id
    const res = await api.get('/api/profiles/' + id + '/vaults')
    logger.log('getmyvaults', res.data)
    AppState.vaults = res.data
  }

  async createKV() {
    const res = await api.get('/api/vaults/' + id + '/keeps')
    logger.log(res.data)
    AppState.vaultkeeps = AppState.vaultkeeps.push(res.data)
  }
}

export const vaultsService = new VaultsService()