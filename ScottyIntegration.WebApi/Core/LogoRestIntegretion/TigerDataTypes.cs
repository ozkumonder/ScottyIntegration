namespace ScottyIntegration.WebApi.Core.LogoRestIntegretion
{
    public enum TigerDataTypes
    {
        items,
        itemSlips,
        purchasedServices,
        salesOrders,
        purchaseOrders,
        purchaseDiscounts,
        purchaseExpenses,
        salesDiscounts,
        salesExpenses,
        purchasePromotions,
        salesPromotions,
        purchasedItemPrices,
        purchasedServicePrices,
        salesItemPrices,
        salesServicePrices,
        salesmen,
        purchaseDispatches,
        salesDispatches,
        purchaseInvoices,
        salesInvoices,
        chequeAndPnotes,
        chequeAndPnoteRolls,
        banks,
        bankAccounts,
        bankSlips = 3,
        GLAccounts,
        GLSlips,
        overheadAccounts,
        safeDeposits,
        safeDepositSlips = 11,
        Arps,
        ArpSlips,
        paymentPlans,
        unitSets,
        ArpShipmentLocations,
        FARegistries,
        itemMLDescriptions,
        ArpMLDescriptons,
        bankMLDescriptions,
        GLAccountMLDescriptions,
        customerMLDescriptions,
        itemAlternatives,
        itemBoms,
        serialAndLotNumbers,
        itemCharacteristics,
        characteristics,
        workstations,
        workstationGroups,
        employees,
        employeeGroups,
        workstationCosts,
        employeeCosts,
        shifts,
        shiftAssignments,
        Boms,
        operations,
        productionRoutes,
        productionParameters,
        QCCriteriaSets,
        deliveryCodes,
        groupCodes,
        salesmanPositionCodes,
        paymentPlanGroupCodes,
        specialCodes,
        authorizationCodes,
        customersOfSalesmen,
        salesmanRoutes,
        salesmanDestinations,
        purchaseCampaigns,
        salesCampaigns,
        vehicles,
        distributionRoutes,
        distributionOrders,
        countries,
        cities,
        postCodes,
        towns,
        districts,
        itemClassAssignments,
        standardCostPeriods,
        itemStandardCosts,
        workstationStandardCosts,
        employeeStandardCosts,
        BomStandardCosts,
        productionExceptions,
        soldServices,
        additionalTaxes,
        productionLines,
        demandPeggings,
        paymentDifferenceInvoices,
        projects,
        repaymentPlans,
        distributionTemplates,
        locationCodes,
        salesConditionsForSlipLines,
        salesConditionsForSlips,
        purchaseConditionsForSlipLines,
        purchaseConditionsForSlips,
        demandSlips,
        exportCredits,
        freeZones,
        customsOffices,
        importOperationSlips,
        exportOperationSlips,
        exportTypedPurchaseInvoices,
        exportTypedSalesInvoices,
        inwardProcessingPermits,
        exportMovementSlips,
        exportNationalizationSlips,
        importDistributionSlips,
        itemBrands,
        extendedFields,
        extendedFieldDefinitions,
        mandatoryFields,
        extendedFieldCategories,
        workflowRoles,
        workflowDefinitions,
        productionResourceUtilization,
        ArpGroupAssignments,
        collateralRolls,
        purchaseProposalOrders,
        purchaseProposalOffers,
        purchaseProposalContracts,
        quickProductionSlips,
        customers,
        salesCategories,
        contacts,
        bankCredits,
        costDistributionSlips,
        characteristicSets,
        variants,
        GLIntegrationCodes,
        engineeringChanges,
        QCCriteriaAssignments,
        FAAssignmentSlips,
        salesOffers,
        salesContracts,
        salesProvisionDistributionSlips,
        stopCauses,
        opportunities,
        salesActivities
    }
    /// <summary>
    /// Tiger Rest Url Parameter
    /// </summary>
    public class TigerDataType
    {
        private TigerDataType(string value) { Value = value; }
        public string Value { get; set; }
        /// <summary>
        /// Malzeme Kartý
        /// </summary>
        public static TigerDataType MalzemeKarti => new TigerDataType("items");
        /// <summary>
        /// Malzeme Hareketleri
        /// </summary>
        public static TigerDataType MalzemeFisi => new TigerDataType("itemSlips");
        /// <summary>
        /// Alýnan Hizmet Kartý
        /// </summary>
        public static TigerDataType PurchasedServices => new TigerDataType("purchasedServices");
        /// <summary>
        /// Kasa Ýþlemleri
        /// </summary>
        public static TigerDataType SafeDepositSlips => new TigerDataType("safeDepositSlips");
        /// <summary>
        /// Cari Hesap Kartý
        /// </summary>
        public static TigerDataType Arps => new TigerDataType("Arps");
        /// <summary>
        /// Cari Hesap Fiþleri
        /// </summary>
        public static TigerDataType ArpSlips => new TigerDataType("ArpSlips");
        /// <summary>
        /// Banka Fiþleri
        /// </summary>
        public static TigerDataType BankSlips => new TigerDataType("bankSlips");
        /// <summary>
        /// Muhasebe Fiþleri
        /// </summary>
        public static TigerDataType GlSlips => new TigerDataType("GLSlips");
        /// <summary>
        /// Proje Kartý
        /// </summary>
        public static TigerDataType Projects => new TigerDataType("projects");
        /// <summary>
        /// Verilen Hizmet Kartý
        /// </summary>
        public static TigerDataType SoldServices => new TigerDataType("soldServices");
        /// <summary>
        /// Muhasebe Hesap Kartý
        /// </summary>
        public static TigerDataType GLAccounts => new TigerDataType("GLAccounts");
        /// <summary>
        /// Satýþ Faturasý
        /// </summary>
        public static TigerDataType SalesInvoices => new TigerDataType("salesInvoices");
        /// <summary>
        /// Satýn Alma Faturasý
        /// </summary>
        public static TigerDataType PurchaseInvoices => new TigerDataType("purchaseInvoices");
    }
}