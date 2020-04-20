

DECLARE @item_code NVARCHAR(20);

DECLARE @location_id INT;

DECLARE @brand_id INT;

DECLARE @user_id INT;

DECLARE @viewer_id INT;

DECLARE @start_date DATE;

DECLARE @end_date DATE;

SET @viewer_id = 203;

--SET @user_id = 223;

--SET @start_date = '17-Aug-2018';

--SET @end_date = '17-Aug-2018';

SELECT COUNT(*)
FROM ( SELECT a.user_id ,
              model_id ,
              CONVERT(DATE , create_time) AS create_time ,
              SUM(quantity) AS               quantity ,
              SUM(value * CASE
                              WHEN ( quantity > 0 )
                              THEN 1
                              ELSE-1
                          END) AS            value
       FROM sc_salesmanage_merchant.merchant_daily_update AS a INNER JOIN db_salesmanage_user.f_getLeastChildrens_byParentUser_Id ( @viewer_id
                                                                                                                                  ) AS v ON a.user_id = v.user_id
       WHERE location_id = isnull(@location_id , location_id)
             AND
             a.user_id = isnull(@user_id , a.user_id)
             AND
             CONVERT(DATE , create_time , 120) BETWEEN CONVERT(DATE , isnull(@start_date , create_time) , 120) AND CONVERT(DATE , isnull(@end_date , create_time) , 120)
       GROUP BY a.user_id ,
                CONVERT(DATE , create_time) ,
                model_id
     ) AS y;

SELECT isnull(ROW_NUMBER() OVER(ORDER BY dly_upd.create_time DESC) , 0) AS ROW_NUM ,
       usr.user_id AS                                                      UserId ,
       brand.brand_id AS                                                   BrandId ,
       category.category_id AS                                             CategoryId ,
       item.model_id AS                                                    ModelId ,
       dly_upd.create_time AS                                              CreateTime ,
       usr.first_name+' '+usr.second_name AS                               name ,
       loc_m.description AS                                                location ,
       brand.description AS                                                brand ,
       category.description AS                                             category ,
       item.description ,
       item.item_code AS                                                   item ,
       dly_upd.quantity ,
       usr.user_name AS                                                    UserName ,
       dly_upd.value AS                                                    TotalValue
FROM ( SELECT a.user_id ,
              model_id ,
              location_id ,
              CONVERT(DATE , create_time) AS create_time ,
              SUM(quantity) AS               quantity ,
              SUM(value * CASE
                              WHEN ( quantity > 0 )
                              THEN 1
                              ELSE-1
                          END) AS            value
       FROM sc_salesmanage_merchant.merchant_daily_update AS a INNER JOIN db_salesmanage_user.f_getLeastChildrens_byParentUser_Id ( @viewer_id
                                                                                                                                  ) AS v ON a.user_id = v.user_id
       WHERE location_id = isnull(@location_id , location_id)
             AND
             a.user_id = isnull(@user_id , a.user_id)
             AND
             CONVERT(DATE , create_time , 120) BETWEEN CONVERT(DATE , isnull(@start_date , create_time) , 120) AND CONVERT(DATE , isnull(@end_date , create_time) , 120)
       GROUP BY a.user_id ,
                location_id ,
                CONVERT(DATE , create_time) ,
                model_id
     ) AS dly_upd LEFT JOIN sc_salesmanage_user.user_m AS usr ON dly_upd.user_id = usr.user_id
                  LEFT JOIN sc_salesmanage_user.location_m AS loc_m ON dly_upd.location_id = loc_m.location_id
                  LEFT JOIN ( SELECT model_id ,
                                     MAX(category_id) AS       category_id ,
                                     MAX(brand_id) AS          brand_id ,
                                     MAX(description) AS       description ,
                                     MAX(item_code) AS         item_code ,
                                     MAX(inventory_item_id) AS inventory_item_id
                              FROM sc_salesmanage_vansale.inventory_item_m
                              GROUP BY model_id
                            ) AS item ON dly_upd.model_id = item.model_id
                  LEFT JOIN sc_salesmanage_merchant.category AS category ON item.category_id = category.category_id
                  LEFT JOIN sc_salesmanage_vansale.brand_m AS brand ON item.brand_id = brand.brand_id
WHERE item.item_code = isnull(@item_code , item.item_code)
      AND
      brand.brand_id = isnull(@brand_id , brand.brand_id)
ORDER BY usr.user_id ,
         create_time ,
         item.model_id;